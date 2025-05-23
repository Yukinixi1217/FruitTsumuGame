using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TruePressedButton : Button
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        // マウスが戻ってきたら Highlighted を強制
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
