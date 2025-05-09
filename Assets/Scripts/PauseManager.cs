using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Button settingButton;     // Settings ボタン
    [SerializeField] private GameObject settingsCanvas; // SettingsCanvas

    private void Start()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false); // 起動時に非表示にする
        }

        if (settingButton != null)
        {
            settingButton.onClick.AddListener(OpenSettingsCanvas);
        }
    }

    private void OpenSettingsCanvas()
    {
        Debug.Log("Settings ボタンが押されました → SettingsCanvas を表示");
        if (settingsCanvas != null)
            settingsCanvas.SetActive(true);
    }
}
