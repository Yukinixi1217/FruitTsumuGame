using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IUIManager
{
    [System.Serializable]
    public class UIGroupEntry
    {
        public string name;
        public CanvasGroup canvasGroup;
    }

    [Header("UI Groups to Manage")]
    [SerializeField] private List<UIGroupEntry> uiGroups = new();

    private Dictionary<string, CanvasGroup> groupDict;

    [Header("Optional Overlay")]
    [SerializeField] private Image dimOverlayImage;

    [Header("DismissArea Buttons")]
    [SerializeField] private Image popupDismissArea;
    [SerializeField] private Image settingDismissArea;
    [SerializeField] private Image treasureDismissArea;

    private readonly HashSet<string> excludedGroupNames = new() { "Home" };

    public void Hide()
    {
        Debug.Log("UIController.Hide() は未実装です");
    }

    private void Awake()
    {
        groupDict = new();

        foreach (var entry in uiGroups)
        {
            if (entry != null && entry.canvasGroup != null && !string.IsNullOrEmpty(entry.name))
            {
                groupDict[entry.name] = entry.canvasGroup;
                Debug.Log($"[登録] uiGroups: {entry.name} → {entry.canvasGroup.name}");
            }
        }

        // 起動直後にすべての CanvasGroup を非表示にする（明示的に設定）
        foreach (var kvp in groupDict)
        {
            var group = kvp.Value;
            bool isHome = excludedGroupNames.Contains(kvp.Key);

            group.alpha = isHome ? 1f : 0f;
            group.interactable = isHome;
            group.blocksRaycasts = isHome;
        }

        SetDismissAreaState(popupDismissArea, false);
        SetDismissAreaState(settingDismissArea, false);
        SetDismissAreaState(treasureDismissArea, false);

        Show("Home");
    }

    private void Start()
    {
        if (popupDismissArea != null && popupDismissArea.TryGetComponent(out Button popupBtn))
            popupBtn.onClick.AddListener(() => { Debug.Log("[Button] PopupDismissArea clicked"); CloseAllPopups(); });

        if (settingDismissArea != null && settingDismissArea.TryGetComponent(out Button settingBtn))
            settingBtn.onClick.AddListener(() => { Debug.Log("[Button] SettingDismissArea clicked"); CloseAllPopups(); });

        if (treasureDismissArea != null && treasureDismissArea.TryGetComponent(out Button treasureBtn))
            treasureBtn.onClick.AddListener(() => { Debug.Log("[Button] TreasureDismissArea clicked"); CloseAllPopups(); });
    }

    public void Show(string targetName)
    {
        Debug.Log($"Show呼び出し: [{targetName}]");

        foreach (var kvp in groupDict)
        {
            bool isTarget = kvp.Key == targetName;
            var group = kvp.Value;

            group.alpha = isTarget ? 1f : 0f;
            group.interactable = isTarget;
            group.blocksRaycasts = isTarget;
        }

        SetDismissAreas(targetName);
    }

    public void ShowPopup(string popupName)
    {
        Debug.Log($"ShowPopup呼び出し: [{popupName}]");

        if (groupDict.TryGetValue(popupName, out var popupGroup))
        {
            popupGroup.alpha = 1f;
            popupGroup.interactable = true;
            popupGroup.blocksRaycasts = true;
        }

        SetDismissAreas(popupName);
    }

    public void CloseAllPopups()
    {
        Debug.Log("=== CloseAllPopups 開始 ===");

        foreach (var kvp in groupDict)
        {
            if (excludedGroupNames.Contains(kvp.Key)) continue;

            var group = kvp.Value;
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;
        }

        SetDismissAreaState(popupDismissArea, false);
        SetDismissAreaState(settingDismissArea, false);
        SetDismissAreaState(treasureDismissArea, false);

        Debug.Log("すべてのポップアップとDismissAreaを無効化完了");
    }

    private void SetDismissAreas(string activeGroup)
    {
        SetDismissAreaState(popupDismissArea, activeGroup == "Popup");
        SetDismissAreaState(settingDismissArea, activeGroup == "Settings");
        SetDismissAreaState(treasureDismissArea, activeGroup == "Treasure");
    }

    private void SetDismissAreaState(Image image, bool state)
    {
        if (image == null) return;

        var cg = image.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.alpha = state ? 1f : 0f;
            cg.interactable = state;
            cg.blocksRaycasts = state;
            cg.ignoreParentGroups = true;
        }

        image.raycastTarget = state;

        if (image.TryGetComponent(out Button button))
            button.interactable = state;

        Debug.Log($"DismissArea設定変更: {image.name} alpha={cg?.alpha} interactable={cg?.interactable} blocksRaycasts={cg?.blocksRaycasts} raycastTarget={image.raycastTarget} button.interactable={button?.interactable}");
    }
}
