using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DismissAreaHandler : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas; // ��\���ɂ������L�����o�X
    [SerializeField] private GameObject returnCanvas; // �\���ɖ߂��Ώہi�C�Ӂj

    // Button�ȊO�̗̈悪�N���b�N���ꂽ�Ƃ��ɌĂ΂��悤�AOnClick�ɐݒ�
    public void OnClickDismiss()
    {
        Debug.Log("[OnClickDismiss] �Ăяo����܂���");

        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        if (clicked != null)
        {
            Debug.Log($"[OnClickDismiss] clicked: {clicked.name}");
        }

        if (clicked != null && clicked.GetComponent<Button>() != null)
        {
            Debug.Log("[OnClickDismiss] Button �������ꂽ���� return");
            return;
        }

        if (targetCanvas != null)
        {
            Debug.Log("[OnClickDismiss] targetCanvas ���\���ɂ��܂�");
            targetCanvas.SetActive(false);
        }

        if (returnCanvas != null)
        {
            Debug.Log("[OnClickDismiss] returnCanvas ��\���ɂ��܂�");
            returnCanvas.SetActive(true);
        }
    }

}
