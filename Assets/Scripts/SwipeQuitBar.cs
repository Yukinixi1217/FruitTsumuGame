using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeQuitBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float startY;
    private const float swipeThreshold = 50f;

    public void OnPointerDown(PointerEventData eventData)
    {
        startY = eventData.position.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float deltaY = eventData.position.y - startY;
        Debug.Log($"[SwipeQuitBar] deltaY = {deltaY}");

#if UNITY_EDITOR
        // �G�f�B�^�ł͏�ɃX���C�v�����Ƃ���i�e�X�g�p�j
        Debug.Log("[SwipeQuitBar] �G�f�B�^�Ȃ̂ő��I���e�X�g���[�h");
        Application.Quit();
#else
        if (deltaY > swipeThreshold)
        {
            Debug.Log("[SwipeQuitBar] �X���C�v�ŏI��");
            Application.Quit();
        }
#endif
    }
}
