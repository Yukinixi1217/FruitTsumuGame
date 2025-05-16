using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour, IUIManager
{
    [SerializeField] private List<CanvasGroup> uiGroups;
    [SerializeField] private Image settingDismissArea;
    [SerializeField] private Image treasureDismissArea;

    private Dictionary<string, CanvasGroup> uiGroupMap = new Dictionary<string, CanvasGroup>();
    public void Hide()
    {
        Debug.Log("GameUIController.Hide() は未実装です");
    }
    public void Show(string name)
    {
        Debug.Log($"GameUIController.Show({name}) 呼び出し");
        // 必要に応じてUIの表示処理を追加
    }

    private void Awake()
    {
        foreach (var group in uiGroups)
        {
            if (group != null)
            {
                string key = group.gameObject.name;
                if (!uiGroupMap.ContainsKey(key))
                {
                    uiGroupMap.Add(key, group);
                    Debug.Log($"[登録] uiGroups: {key} → {group.gameObject.name}");
                }
            }
        }

        CloseAllPopups();

        foreach (var kvp in uiGroupMap)
        {
            Debug.Log($"[確認] {kvp.Key} alpha: {kvp.Value.alpha}");
        }
    }

    public void ShowPopup(string popupName)
    {
        Debug.Log($"[ShowPopup] 表示: {popupName}");
        StartCoroutine(ShowPopupAfterDelay(popupName));
    }

    private IEnumerator ShowPopupAfterDelay(string popupName)
    {
        yield return new WaitForSeconds(0.05f);

        foreach (var kvp in uiGroupMap)
        {
            bool isTarget = kvp.Key == popupName;
            kvp.Value.alpha = isTarget ? 1f : 0f;
            kvp.Value.interactable = isTarget;
            kvp.Value.blocksRaycasts = isTarget;

            if (isTarget)
                Debug.Log($"[ShowPopup] {popupName} を表示しました");
        }

        SetDismissAreas("SettingDismissArea", popupName == "SettingsPanel");
        SetDismissAreas("TreasureDismissArea", popupName == "TreasurePanel");
    }

    public void CloseAllPopups()
    {
        Debug.Log("=== CloseAllPopups 開始 ===");

        foreach (var kvp in uiGroupMap)
        {
            var group = kvp.Value;
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;
        }

        SetDismissAreas("SettingDismissArea", false);
        SetDismissAreas("TreasureDismissArea", false);

        Debug.Log("=== CloseAllPopups 終了 ===");
    }

    private void SetDismissAreas(string areaName, bool state)
    {
        Image target = null;
        if (areaName == "SettingDismissArea") target = settingDismissArea;
        else if (areaName == "TreasureDismissArea") target = treasureDismissArea;

        if (target != null)
        {
            SetDismissAreaState(target, state);
        }
    }

    private void SetDismissAreaState(Image image, bool state)
    {
        image.canvasRenderer.SetAlpha(state ? 1f : 0f);

        if (image.TryGetComponent(out Button button))
        {
            button.interactable = state;
        }

        image.raycastTarget = state;

        Debug.Log($"DismissArea設定変更: {image.name} alpha={(state ? 1 : 0)} interactable={state} blocksRaycasts={state} raycastTarget={state} button.interactable={state}");
    }
}
