using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureDismissArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject treasurePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clicked = eventData.pointerCurrentRaycast.gameObject;

        // �{�^���������疳��
        if (clicked != null && clicked.GetComponent<UnityEngine.UI.Button>() != null)
            return;

        // ���鏈��
        if (treasurePanel != null)
        {
            treasurePanel.SetActive(false);
        }
    }
}
