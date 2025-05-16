using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaWithSwipeQuit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float startY;
    private const float swipeThreshold = 50f;

    private RectTransform swipeBar;

    void Start()
    {
        // SafeAreaの取得と適用
        RectTransform rt = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;

        // スワイプ用バーの生成
        GameObject swipeObject = new GameObject("SwipeQuitBar", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        swipeObject.transform.SetParent(transform, false);

        swipeBar = swipeObject.GetComponent<RectTransform>();
        swipeBar.anchorMin = new Vector2(0.5f, 0f);
        swipeBar.anchorMax = new Vector2(0.5f, 0f);
        swipeBar.pivot = new Vector2(0.5f, 0.5f);
        swipeBar.sizeDelta = new Vector2(300f, 30f);
        swipeBar.anchoredPosition = new Vector2(0f, 50f);

        Image img = swipeObject.GetComponent<Image>();
        img.color = new Color(0f, 0f, 0f, 1f);
        img.raycastTarget = true;

        swipeObject.AddComponent<SwipeQuitBar>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startY = eventData.position.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float deltaY = eventData.position.y - startY;
        if (deltaY > swipeThreshold)
        {
            Debug.Log("[SafeAreaWithSwipeQuit] スワイプで終了");
            Application.Quit();
        }
    }
}
