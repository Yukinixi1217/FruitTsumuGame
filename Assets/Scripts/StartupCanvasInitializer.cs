using UnityEngine;
using UnityEngine.InputSystem;

public class StartupCanvasInitializer : MonoBehaviour
{
    public GameObject homeMenuCanvas;
    public GameObject gamePopupCanvas;
    public GameObject homeSettingCanvas;
    public GameObject gamePopupPanel;

    void Start()
    {
#if UNITY_2023_1_OR_NEWER
        var playerInput = FindFirstObjectByType<PlayerInput>();
#else
    var playerInput = FindObjectOfType<PlayerInput>();
#endif
        // GamePopupCanvas 全体を非表示（安全策）
        if (gamePopupCanvas != null)
            gamePopupCanvas.SetActive(false);

        // GamePopupPanel も個別に遮断（CanvasGroupがある場合）
        if (gamePopupPanel != null)
        {
            var cg = gamePopupPanel.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.interactable = false;
                cg.blocksRaycasts = false;
                cg.alpha = 0f;
            }
        }

        if (playerInput != null)
        {
            Debug.Log("現在のActionMap: " + playerInput.currentActionMap.name);

            if (playerInput.currentActionMap.name != "UI")
            {
                Debug.Log("ActionMapをUIに切り替えます");
                playerInput.SwitchCurrentActionMap("UI");
            }
        }
    }
}
