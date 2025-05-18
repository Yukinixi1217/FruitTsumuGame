using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour, IUIManager
{
    [System.Serializable]
    public class UIGroupEntry
    {
        public string name; // "Settings" や "Treasure"
        public CanvasGroup canvasGroup;
        public RectTransform panelTransform;
    }

    [SerializeField] private List<UIGroupEntry> uiGroups = new List<UIGroupEntry>();
    private Dictionary<string, UIGroupEntry> groupDict;

    [Header("Dismiss Areas")]
    [SerializeField] private Image SettingDismissArea;
    [SerializeField] private Image TreasureDismissArea;

    [Header("Buttons")]
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button AddCoinButton;

    private void Awake()
    {
        groupDict = new Dictionary<string, UIGroupEntry>();

        foreach (var entry in uiGroups)
        {
            if (entry != null && !string.IsNullOrEmpty(entry.name) && entry.canvasGroup != null)
            {
                groupDict[entry.name] = entry;
                Debug.Log($"[登録] uiGroups: {entry.name} → {entry.canvasGroup.gameObject.name}");
            }
            else
            {
                Debug.LogError("❌ CanvasGroup が null または name が未設定のエントリがあります");
            }
        }
    }

    private void Start()
    {
        if (SettingButton == null)
            Debug.LogError("❌ SettingButton が未設定です");
        else
            SettingButton.onClick.AddListener(() => Show("Settings"));

        if (AddCoinButton == null)
            Debug.LogError("❌ AddCoinButton が未設定です");
        else
            AddCoinButton.onClick.AddListener(() => Show("Treasure"));

        if (SettingDismissArea != null)
            SettingDismissArea.GetComponent<Button>().onClick.AddListener(CloseAllPopups);
        if (TreasureDismissArea != null)
            TreasureDismissArea.GetComponent<Button>().onClick.AddListener(CloseAllPopups);

        // 起動直後にすべて非表示（CanvasGroup を即時リセット）
        foreach (var kvp in groupDict)
        {
            var entry = kvp.Value;
            entry.canvasGroup.alpha = 0;
            entry.canvasGroup.interactable = false;
            entry.canvasGroup.blocksRaycasts = false;
            entry.panelTransform.localScale = Vector3.zero;
        }
        SetDismissAreaState("Settings", false);
        SetDismissAreaState("Treasure", false);
    }

    public void Show(string name)
    {
        CloseAllPopups();
        if (groupDict.TryGetValue(name, out var group))
        {
            Debug.Log($"[表示] {name}");
            LeanTween.cancel(group.panelTransform.gameObject);
            group.panelTransform.localScale = Vector3.zero;
            group.canvasGroup.alpha = 1;
            group.canvasGroup.interactable = true;
            group.canvasGroup.blocksRaycasts = true;
            LeanTween.scale(group.panelTransform, Vector3.one, 0.25f).setEaseOutBack();
            SetDismissAreaState(name, true);
        }
        else
        {
            Debug.LogError($"❌ Show: グループが見つかりません → {name}");
        }
    }

    public void Hide()
    {
        CloseAllPopups();
    }

    public void CloseAllPopups()
    {
        Debug.Log("=== CloseAllPopups 開始 ===");
        foreach (var kvp in groupDict)
        {
            var entry = kvp.Value;
            Debug.Log($"[非表示] {kvp.Key}");
            LeanTween.cancel(entry.panelTransform.gameObject);
            LeanTween.scale(entry.panelTransform, Vector3.zero, 0.25f).setEaseInBack().setOnComplete(() =>
            {
                entry.canvasGroup.alpha = 0;
                entry.canvasGroup.interactable = false;
                entry.canvasGroup.blocksRaycasts = false;
            });
        }
        SetDismissAreaState("Settings", false);
        SetDismissAreaState("Treasure", false);
        Debug.Log("=== CloseAllPopups 終了 ===");
    }

    private void SetDismissAreaState(string groupName, bool state)
    {
        Image area = null;
        if (groupName == "Settings") area = SettingDismissArea;
        if (groupName == "Treasure") area = TreasureDismissArea;

        if (area != null)
        {
            area.GetComponent<Button>().interactable = state;
            area.raycastTarget = state;
            Debug.Log($"DismissArea設定変更: {area.name} interactable={state} raycastTarget={state}");
        }
    }
}
