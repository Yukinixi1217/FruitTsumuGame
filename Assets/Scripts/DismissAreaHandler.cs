using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DismissAreaHandler : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas; // 非表示にしたいキャンバス
    [SerializeField] private GameObject returnCanvas; // 表示に戻す対象（任意）

    // Button以外の領域がクリックされたときに呼ばれるよう、OnClickに設定
    public void OnClickDismiss()
    {
        Debug.Log("[OnClickDismiss] 呼び出されました");

        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        if (clicked != null)
        {
            Debug.Log($"[OnClickDismiss] clicked: {clicked.name}");
        }

        if (clicked != null && clicked.GetComponent<Button>() != null)
        {
            Debug.Log("[OnClickDismiss] Button が押されたため return");
            return;
        }

        if (targetCanvas != null)
        {
            Debug.Log("[OnClickDismiss] targetCanvas を非表示にします");
            targetCanvas.SetActive(false);
        }

        if (returnCanvas != null)
        {
            Debug.Log("[OnClickDismiss] returnCanvas を表示にします");
            returnCanvas.SetActive(true);
        }
    }

}
