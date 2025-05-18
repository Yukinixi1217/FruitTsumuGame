using System.Collections;
using UnityEngine;
using TMPro;

public class RewardPopupController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup popupCanvasGroup;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private RectTransform panelTransform;

    [Header("Animation Settings")]
    [SerializeField] private float popupScale = 1f;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float autoHideDelay = 2f;
    [SerializeField] private bool debugSlowMode = false;

    private IUIManager uiManager;

    private void Start()
    {
        panelTransform.localScale = Vector3.zero; // ★強制リセット追加
        Debug.Log($"[Start] 初期スケール: {panelTransform.localScale}");
        uiManager = FindFirstUIManager();
        if (uiManager == null)
            Debug.LogError("❌ UIManager が見つかりません！");
        HideInstant();
    }

    public void Show(int rewardAmount)
{
    Debug.Log("💡 Show() 開始");

    // 状態リセット（先に完全非表示状態に）
    popupCanvasGroup.alpha = 0;
    popupCanvasGroup.interactable = false;
    popupCanvasGroup.blocksRaycasts = false;
    panelTransform.localScale = Vector3.zero;
    panelTransform.gameObject.SetActive(true); // 明示的に表示化

    // 表示値セット
    rewardText.text = $"+{rewardAmount} コイン";

    float actualDuration = debugSlowMode ? duration * 10f : duration;

    // 表示開始（ここで一気にアニメ）
    LeanTween.scale(panelTransform, Vector3.one, actualDuration)
        .setEaseOutBack()
        .setOnStart(() =>
        {
            popupCanvasGroup.alpha = 1;
            popupCanvasGroup.interactable = true;
            popupCanvasGroup.blocksRaycasts = true;
        })
        .setOnUpdate((Vector3 val) =>
        {
            Debug.Log($"🔍 中間スケール（拡大）: {val}");
        })
        .setOnComplete(() =>
        {
            Debug.Log("✅ スケールアニメーション完了 → 表示状態");
        });

    StartCoroutine(HideAfterDelay(actualDuration + autoHideDelay));
}

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Hide();
    }

    public void Hide()
    {
        float actualDuration = debugSlowMode ? duration * 10f : duration;

        Debug.Log($"🔽 スケールアニメーション開始 → 1 → 0 (duration = {actualDuration})");

        LeanTween.scale(panelTransform.gameObject, Vector3.zero, actualDuration)
            .setEaseInBack()
            .setOnUpdate((Vector3 val) => {
                Debug.Log($"🔍 中間スケール（縮小）: {val}");
            })
            .setOnComplete(() => {
                popupCanvasGroup.alpha = 0;
                popupCanvasGroup.interactable = false;
                popupCanvasGroup.blocksRaycasts = false;
                Debug.Log("🟢 ポップアップ非表示完了");
            });
    }

    private void HideInstant()
    {
        popupCanvasGroup.alpha = 0;
        popupCanvasGroup.interactable = false;
        popupCanvasGroup.blocksRaycasts = false;
        panelTransform.localScale = Vector3.zero;
    }

    private IUIManager FindFirstUIManager()
    {
        foreach (var mb in FindObjectsOfType<MonoBehaviour>())
            if (mb is IUIManager manager)
                return manager;
        return null;
    }
}
