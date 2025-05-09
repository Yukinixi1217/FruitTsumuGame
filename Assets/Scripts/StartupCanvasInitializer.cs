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
        // GamePopupCanvas �S�̂��\���i���S��j
        if (gamePopupCanvas != null)
            gamePopupCanvas.SetActive(false);

        // GamePopupPanel ���ʂɎՒf�iCanvasGroup������ꍇ�j
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
            Debug.Log("���݂�ActionMap: " + playerInput.currentActionMap.name);

            if (playerInput.currentActionMap.name != "UI")
            {
                Debug.Log("ActionMap��UI�ɐ؂�ւ��܂�");
                playerInput.SwitchCurrentActionMap("UI");
            }
        }
    }
}
