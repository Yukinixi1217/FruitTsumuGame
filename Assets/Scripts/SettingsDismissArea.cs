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

        // Button �������ꂽ�Ȃ�L�����Z���i���Ȃ��j
        if (clicked != null && clicked.GetComponent<Button>() != null)
        {
            return;
        }

        // ����ȊO�̗̈悪�^�b�v���ꂽ�����
        if (settingsCanvas != null) settingsCanvas.SetActive(false);
        if (homeCanvas != null) homeCanvas.SetActive(true);
    }
}
