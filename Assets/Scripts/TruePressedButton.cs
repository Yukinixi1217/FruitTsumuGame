using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TruePressedButton : Button
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        // ƒ}ƒEƒX‚ª–ß‚Á‚Ä‚«‚½‚ç Highlighted ‚ğ‹­§
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left && Input.GetMouseButton(0))
        {
            DoStateTransition(SelectionState.Pressed, false);
        }
        else
        {
            DoStateTransition(SelectionState.Highlighted, false);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        DoStateTransition(SelectionState.Normal, false);
    }
}
