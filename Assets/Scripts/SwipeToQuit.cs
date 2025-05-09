using UnityEngine;

public class SwipeToQuit : MonoBehaviour
{
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private float swipeThreshold = 100f; // �X���C�v�Ƃ��ĔF�����鋗���i�s�N�Z���j

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPos = touch.position;
                    float swipeDist = endTouchPos.y - startTouchPos.y;

                    // ��X���C�v�Ȃ�I��������
                    if (swipeDist > swipeThreshold && Mathf.Abs(endTouchPos.x - startTouchPos.x) < 100f)
                    {
                        Debug.Log("��X���C�v�ŏI���I");
                        QuitApp();
                    }
                    break;
            }
        }
    }

    private void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                                             .GetStatic<AndroidJavaObject>("currentActivity"))
        {
            activity.Call("finish");
        }
#else
        Application.Quit();
#endif
    }
}
