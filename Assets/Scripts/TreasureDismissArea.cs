using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureDismissArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject treasurePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clicked = eventData.pointerCurrentRaycast.gameObject;

        // ƒ{ƒ^ƒ“‚¾‚Á‚½‚ç–³‹
        if (clicked != null && clicked.GetComponent<UnityEngine.UI.Button>() != null)
            return;

        // •Â‚¶‚éˆ—
        if (treasurePanel != null)
        {
            treasurePanel.SetActive(false);
        }
    }
}
