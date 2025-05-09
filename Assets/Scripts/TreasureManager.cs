using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    [SerializeField] private GameObject treasurePanel;

    private void Start()
    {
        if (treasurePanel == null)
            Debug.LogError("TreasureManager: treasurePanel ‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
        else
            treasurePanel.SetActive(false); // ‰Šú”ñ•\¦
    }

    public void ShowTreasurePanel()
    {
        if (treasurePanel != null)
        {
            treasurePanel.SetActive(true);
        }
    }
}
