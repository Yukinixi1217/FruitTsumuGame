using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    [SerializeField] private GameObject treasurePanel;

    private void Start()
    {
        if (treasurePanel == null)
            Debug.LogError("TreasureManager: treasurePanel が設定されていません");
        else
            treasurePanel.SetActive(false); // 初期非表示
    }

    public void ShowTreasurePanel()
    {
        if (treasurePanel != null)
        {
            treasurePanel.SetActive(true);
        }
    }
}
