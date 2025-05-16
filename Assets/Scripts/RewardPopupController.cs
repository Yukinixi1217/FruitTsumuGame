using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RewardPopupController : MonoBehaviour
{
    [SerializeField] private CanvasGroup popupCanvasGroup;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private float showDuration = 2f;

    private IUIManager uiManager;

    private IEnumerator Start()
    {
        yield return null; // 1フレーム待って確実にUIControllerを取得

        var uiController = FindFirstObjectByType<UIController>();
        var gameUiController = FindFirstObjectByType<GameUIController>();

        if (uiController is IUIManager)
        {
            uiManager = (IUIManager)uiController;
        }
        else if (gameUiController is IUIManager)
        {
            uiManager = (IUIManager)gameUiController;
        }
        else
        {
            Debug.LogError("❌ UIManager が見つかりません！");
            yield break;
        }

        // 初期状態を非表示にする
        if (popupCanvasGroup != null)
        {
            popupCanvasGroup.alpha = 0;
            popupCanvasGroup.interactable = false;
            popupCanvasGroup.blocksRaycasts = false;
        }
    }

    public void Show(int amount)
    {
        if (popupCanvasGroup == null) return;

        rewardText.text = $"+{amount} コイン";
        Debug.Log($"[DEBUG] ポップアップ表示: +{amount} コイン");

        // 表示（フェードイン）
        popupCanvasGroup.alpha = 1;
        popupCanvasGroup.interactable = true;
        popupCanvasGroup.blocksRaycasts = true;

        LeanTween.cancel(gameObject);
        LeanTween.delayedCall(gameObject, showDuration, () => Hide());
    }

    private void Hide()
    {
        if (popupCanvasGroup == null) return;

        // 非表示（フェードアウト）
        LeanTween.alphaCanvas(popupCanvasGroup, 0, 0.5f).setOnComplete(() =>
        {
            popupCanvasGroup.interactable = false;
            popupCanvasGroup.blocksRaycasts = false;
            Debug.Log("🟢 ポップアップ非表示完了");
        });
    }
}
