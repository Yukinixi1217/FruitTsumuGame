using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeQuitBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float startY;
    private const float swipeThreshold = 50f;

    public void OnPointerDown(PointerEventData eventData)
    {
        startY = eventData.position.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float deltaY = eventData.position.y - startY;
        Debug.Log($"[SwipeQuitBar] deltaY = {deltaY}");

#if UNITY_EDITOR
        // エディタでは常にスワイプ成功とする（テスト用）
        Debug.Log("[SwipeQuitBar] エディタなので即終了テストモード");
        Application.Quit();
#else
        if (deltaY > swipeThreshold)
        {
            Debug.Log("[SwipeQuitBar] スワイプで終了");
            Application.Quit();
        }
#endif
    }
}
