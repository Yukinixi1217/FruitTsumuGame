using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image popupDismissAreaImage;
    [SerializeField] private Image settingDismissAreaImage;
    [SerializeField] private Image treasureDismissAreaImage;

    [SerializeField] private Button popupDismissAreaButton;
    [SerializeField] private Button settingDismissAreaButton;
    [SerializeField] private Button treasureDismissAreaButton;

    private Dictionary<string, CanvasGroup> groupDict = new Dictionary<string, CanvasGroup>();

    private string currentGroupName = "Home";

    private void Awake()
    {
        // シーン内の全CanvasGroupを取得し、名前で管理辞書に登録
        foreach (var group in GetComponentsInChildren<CanvasGroup>(true))
        {
            string name = group.gameObject.name.Replace("Canvas", "");
            groupDict[name] = group;
            Debug.Log($"登録: {name} → {group.gameObject.name}");
        }

        // 起動時にHome画面を表示
        Show("Home");
    }

    private void Start()
    {
        // Dismissボタンにクリックイベントを設定
        if (popupDismissAreaButton != null)
        {
            popupDismissAreaButton.onClick.AddListener(() =>
            {
                Debug.Log("[Button] PopupDismissArea clicked");
                CloseAllPopups();
            });
        }

        if (settingDismissAreaButton != null)
        {
            settingDismissAreaButton.onClick.AddListener(() =>
            {
                Debug.Log("[Button] SettingDismissArea clicked");
                CloseAllPopups();
            });
        }

        if (treasureDismissAreaButton != null)
        {
            treasureDismissAreaButton.onClick.AddListener(() =>
            {
                Debug.Log("[Button] TreasureDismissArea clicked");
                CloseAllPopups();
            });
        }

    }

    // 指定のUIグループを表示（初期表示やメニュー切替）
    public void Show(string targetGroupName)
    {
        Debug.Log($"Show呼び出し: [{targetGroupName}]");

        foreach (var entry in groupDict)
        {
            SetGroupState(entry.Key, entry.Value, targetGroupName);
        }

        SetDismissAreas(targetGroupName);
        currentGroupName = targetGroupName;
    }

    // ポップアップ専用表示メソッド（Showと挙動は同じ）
    public void ShowPopup(string targetGroupName)
    {
        Debug.Log($"ShowPopup呼び出し: [{targetGroupName}]");

        foreach (var entry in groupDict)
        {
            SetGroupState(entry.Key, entry.Value, targetGroupName);
        }

        SetDismissAreas(targetGroupName);
        currentGroupName = targetGroupName;
    }

    // UI表示状態を切り替える（CanvasGroup制御）
    private void SetGroupState(string groupName, CanvasGroup group, string targetGroupName)
    {
        if (group == null) return;

        bool isTarget = groupName == targetGroupName;

        group.alpha = isTarget ? 1f : 0f;
        group.interactable = isTarget;
        group.blocksRaycasts = isTarget;
        group.gameObject.SetActive(true);
    }

    // 現在アクティブなグループに応じてDismissエリアを有効化
    private void SetDismissAreas(string activeGroup)
    {
        SetDismissAreaState(popupDismissAreaImage, activeGroup == "Popup");
        SetDismissAreaState(settingDismissAreaImage, activeGroup == "Settings");
        SetDismissAreaState(treasureDismissAreaImage, activeGroup == "Treasure");
    }

    // 各Dismiss画像エリアに対するUI設定切り替え処理
    private void SetDismissAreaState(Image dismissArea, bool isActive)
    {
        if (dismissArea == null) return;

        dismissArea.raycastTarget = isActive;

        var cg = dismissArea.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.alpha = isActive ? 1f : 0f;
            cg.interactable = isActive;
            cg.blocksRaycasts = isActive;
            cg.ignoreParentGroups = true;
        }

        var btn = dismissArea.GetComponent<Button>();
        if (btn != null)
        {
            btn.interactable = isActive;
        }

        Debug.Log($"DismissArea設定変更: {dismissArea.name} alpha={cg?.alpha} interactable={cg?.interactable} blocksRaycasts={cg?.blocksRaycasts} raycastTarget={dismissArea.raycastTarget} button.interactable={btn?.interactable}");
    }

    // 全ポップアップを閉じる（Home以外を非表示）
    public void CloseAllPopups()
    {
        Debug.Log("CloseAllPopups開始");

        foreach (var entry in groupDict)
        {
            if (entry.Key != "Home")
            {
                entry.Value.alpha = 0f;
                entry.Value.interactable = false;
                entry.Value.blocksRaycasts = false;
            }
        }

        SetDismissAreas("None");
        Debug.Log("すべてのポップアップとDismissAreaを無効化完了");
    }
}
