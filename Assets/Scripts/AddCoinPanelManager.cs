using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddCoinPanelManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Button addCoinButton;
    [SerializeField] private GameObject addCoinPanel;       // �R�C���w���p�l���S��
    [SerializeField] private GameObject coinItemButtonsRoot; // CoinItem �{�^���̐e�i��FContent�j

    private void Start()
    {
        if (addCoinButton != null)
            addCoinButton.onClick.AddListener(ShowAddCoinPanel);

        if (addCoinPanel != null)
            addCoinPanel.SetActive(false);  // ������\��
    }

    private void ShowAddCoinPanel()
    {
        Debug.Log("AddCoin �p�l���\��");
        if (addCoinPanel != null)
            addCoinPanel.SetActive(true);
    }

    private void HideAddCoinPanel()
    {
        Debug.Log("AddCoin �p�l�������");
        if (addCoinPanel != null)
            addCoinPanel.SetActive(false);
    }

    // �w�i�Ȃǃp�l���̋󔒂��^�b�v�����Ƃ�
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        // CoinItem �̎q�v�f�������疳���i���Ȃ��j
        if (clickedObject != null && clickedObject.transform.IsChildOf(coinItemButtonsRoot.transform))
        {
            Debug.Log("CoinItem �^�b�v �� �p�l���͕��Ȃ�");
            return;
        }

        // ����ȊO�i�w�i�Ȃǁj�Ȃ����
        HideAddCoinPanel();
    }
}
