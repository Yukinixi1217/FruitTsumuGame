using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUIController : MonoBehaviour
{
    [System.Serializable]
    public class UIGroupEntry
    {
        public string name;
        public CanvasGroup canvasGroup;
    }

    [Header("GameScene 用 UIグループ")]
    [SerializeField] private List<UIGroupEntry> uiGroups = new List<UIGroupEntry>();

    [Header("オーバーレイ（暗転など）")]
    [SerializeField] private Image dimOverlayImage;

    [Header("除外するグループ名（閉じないUI）")]
    [SerializeField] private List<string> excludedGroupNames = new List<string>();

    private Dictionary<string, CanvasGroup> groupDict;

    [Header("DismissArea（タップで閉じる背景）")]
    [SerializeField] private Image SettingDismissArea;
    [SerializeField] private Image TreasureDismissArea;

    private void Awake()
    {
        groupDict = new Dictionary<string, CanvasGroup>();

        foreach (var entry in uiGroups)
        {
            if (entry != null && entry.canvasGroup != null && !string.IsNullOrEmpty(entry.name))
            {
                groupDict[entry.name] = entry.canvasGroup;
                Debug.Log($"[groupDict登録] name={entry.name}, from={entry.canvasGroup.gameObject.name}");
            }
        }

        Show("BackGround");
    }

    private void Start()
    {
        Debug.Log("🔍 GameUIController.Start() 開始");

        if (SettingDismissArea != null)
        {
            Debug.Log("✅ SettingDismissArea 参照あり");

            var button = SettingDismissArea.GetComponent<Button>();
            if (button != null)
            {
                Debug.Log("✅ SettingDismissArea に Button あり → リスナー登録中");

                button.onClick.AddListener(() =>
                {
                    Debug.Log("[Button] SettingDismissArea clicked");
                    CloseAllPopups();
                });
            }
            else
            {
                Debug.LogWarning("⚠ SettingDismissArea に Button がない");
            }
        }
        else
        {
            Debug.LogWarning("⚠ SettingDismissArea が null です");
        }
    }

    public void Show(string groupName)
    {
        Debug.Log($"Show呼び出し: [{groupName}]");

        foreach (var kv in groupDict)
        {
            bool isTarget = kv.Key == groupName;
            kv.Value.alpha = isTarget ? 1 : 0;
            kv.Value.interactable = isTarget;
            kv.Value.blocksRaycasts = isTarget;
        }

        SetDismissAreas(groupName);
    }

    public void ShowPopup(string groupName)
    {
        Debug.Log($"ShowPopup呼び出し: [{groupName}]");

        foreach (var kv in groupDict)
        {
            if (excludedGroupNames.Contains(kv.Key)) continue;

            bool isTarget = kv.Key == groupName;
            kv.Value.alpha = isTarget ? 1 : 0;
            kv.Value.interactable = isTarget;
            kv.Value.blocksRaycasts = isTarget;
        }

        SetDismissAreas(groupName);
    }

    private void SetDismissAreas(string groupName)
    {
        SetDismissAreaState(SettingDismissArea, groupName == "Settings");
        SetDismissAreaState(TreasureDismissArea, groupName == "Treasure");
    }

    private void SetDismissAreaState(Image area, bool enabled)
    {
        if (area == null) return;

        var cg = area.GetComponent<CanvasGroup>();
        var btn = area.GetComponent<Button>();

        if (cg != null)
        {
            cg.alpha = enabled ? 1 : 0;
            cg.interactable = enabled;
            cg.blocksRaycasts = enabled;
        }

        if (btn != null)
        {
            btn.interactable = enabled;
        }

        area.raycastTarget = enabled;

        Debug.Log($"DismissArea設定変更: {area.name} alpha={cg?.alpha} interactable={cg?.interactable} blocksRaycasts={cg?.blocksRaycasts} raycastTarget={area.raycastTarget} button.interactable={btn?.interactable}");
    }

    public void CloseAllPopups()
    {
        Debug.Log("=== CloseAllPopups 開始 ===");

        foreach (var kv in groupDict)
        {
            if (excludedGroupNames.Contains(kv.Key)) continue;

            kv.Value.alpha = 0;
            kv.Value.interactable = false;
            kv.Value.blocksRaycasts = false;
        }

        SetDismissAreas("");

        Debug.Log("=== CloseAllPopups 終了 ===");
    }
}
