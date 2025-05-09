using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    [SerializeField] private GameObject treasurePanel;

    private void Start()
    {
        if (treasurePanel == null)
            Debug.LogError("TreasureManager: treasurePanel ���ݒ肳��Ă��܂���");
        else
            treasurePanel.SetActive(false); // ������\��
    }

    public void ShowTreasurePanel()
    {
        if (treasurePanel != null)
        {
            treasurePanel.SetActive(true);
        }
    }
}
