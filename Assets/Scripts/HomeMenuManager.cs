using UnityEngine;
using UnityEngine.UI;

public class HomeMenuManager : MonoBehaviour
{
    [Header("�{�^���ڑ�")]
    public Button playButton;
    public Button addCoinButton;
    public Button settingButton;

    [Header("�p�l���ڑ�")]
    public GameObject popupPanel;
    public GameObject treasurePanel;
    public GameObject settingsPanel;

    [Header("Dismiss�G���A�ڑ�")]
    public Button popupDismissArea;
    public Button treasureDismissArea;
    public Button settingsDismissArea;

    [Header("���̑��ڑ�")]
    public GameObject dimOverlayImage;
    public CanvasGroup homeMenuCanvasGroup;

    private void Start()
    {
        playButton.onClick.AddListener(ShowPopupPanel);
        addCoinButton.onClick.AddListener(ShowTreasurePanel);
        settingButton.onClick.AddListener(ShowSettingsPanel);

        popupDismissArea.onClick.AddListener(HideAllPanels);
        treasureDismissArea.onClick.AddListener(HideAllPanels);
        settingsDismissArea.onClick.AddListener(HideAllPanels);

        HideAllPanels();
    }

    private void ShowPopupPanel()
    {
        HideAllPanels();
        if (popupPanel != null) popupPanel.SetActive(true);
        EnableDimOverlayAndDisableMenu();
    }

    private void ShowTreasurePanel()
    {
        HideAllPanels();
        if (treasurePanel != null) treasurePanel.SetActive(true);
        EnableDimOverlayAndDisableMenu();
    }

    private void ShowSettingsPanel()
    {
        HideAllPanels();
        if (settingsPanel != null) settingsPanel.SetActive(true);
        EnableDimOverlayAndDisableMenu();
    }

    private void HideAllPanels()
    {
        if (popupPanel != null) popupPanel.SetActive(false);
        if (treasurePanel != null) treasurePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);

        if (dimOverlayImage != null)
            dimOverlayImage.SetActive(false);

        if (homeMenuCanvasGroup != null)
        {
            homeMenuCanvasGroup.alpha = 1f;
            homeMenuCanvasGroup.interactable = true;
            homeMenuCanvasGroup.blocksRaycasts = true;
        }
    }

    private void EnableDimOverlayAndDisableMenu()
    {
        if (dimOverlayImage != null)
            dimOverlayImage.SetActive(true);

        if (homeMenuCanvasGroup != null)
        {
            homeMenuCanvasGroup.alpha = 1f;
            homeMenuCanvasGroup.interactable = false;
            homeMenuCanvasGroup.blocksRaycasts = false;
        }
    }
}
