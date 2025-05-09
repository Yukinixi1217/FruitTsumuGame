using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingDismissArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject homeCanvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clicked = eventData.pointerCurrentRaycast.gameObject;

        // Button が押されたならキャンセル（閉じない）
        if (clicked != null && clicked.GetComponent<Button>() != null)
        {
            return;
        }

        // それ以外の領域がタップされたら閉じる
        if (settingsCanvas != null) settingsCanvas.SetActive(false);
        if (homeCanvas != null) homeCanvas.SetActive(true);
    }
}
