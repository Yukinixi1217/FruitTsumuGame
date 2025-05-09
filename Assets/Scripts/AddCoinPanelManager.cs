using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddCoinPanelManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Button addCoinButton;
    [SerializeField] private GameObject addCoinPanel;       // コイン購入パネル全体
    [SerializeField] private GameObject coinItemButtonsRoot; // CoinItem ボタンの親（例：Content）

    private void Start()
    {
        if (addCoinButton != null)
            addCoinButton.onClick.AddListener(ShowAddCoinPanel);

        if (addCoinPanel != null)
            addCoinPanel.SetActive(false);  // 初期非表示
    }

    private void ShowAddCoinPanel()
    {
        Debug.Log("AddCoin パネル表示");
        if (addCoinPanel != null)
            addCoinPanel.SetActive(true);
    }

    private void HideAddCoinPanel()
    {
        Debug.Log("AddCoin パネルを閉じる");
        if (addCoinPanel != null)
            addCoinPanel.SetActive(false);
    }

    // 背景などパネルの空白をタップしたとき
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        // CoinItem の子要素だったら無視（閉じない）
        if (clickedObject != null && clickedObject.transform.IsChildOf(coinItemButtonsRoot.transform))
        {
            Debug.Log("CoinItem タップ → パネルは閉じない");
            return;
        }

        // それ以外（背景など）なら閉じる
        HideAddCoinPanel();
    }
}
