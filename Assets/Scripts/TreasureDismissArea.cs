using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreasureDismissArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject treasureCanvas;
    [SerializeField] private GameObject homeCanvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clicked = eventData.pointerCurrentRaycast.gameObject;

        if (clicked != null && clicked.GetComponent<Button>() != null)
            return;

        if (treasureCanvas != null) treasureCanvas.SetActive(false);
        if (homeCanvas != null) homeCanvas.SetActive(true);
    }

}
