using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance { get; private set; }

    [SerializeField] private bool vibrationEnabled = true;

#if UNITY_ANDROID
    private AndroidJavaObject vibrator;
    private AndroidJavaObject unityActivity;
#endif

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibratorService = activity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            vibrator = vibratorService;
        }
        Debug.Log("✔️ Android Vibration 初期化完了");
#else
        Debug.Log("⚠️ Android以外ではバイブ機能は無効です");
#endif
    }

    public void Vibrate(int milliseconds = 50)
    {
        if (!vibrationEnabled) return;

#if UNITY_ANDROID
        vibrator?.Call("vibrate", milliseconds);
#elif UNITY_IOS
        Handheld.Vibrate();
#else
        Debug.Log("Vibration not supported on this platform.");
#endif
    }

    public void VibrateShort()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    Handheld.Vibrate();
    Debug.Log("📳 VibrateShort 実行（Android 実機）");
#else
        Debug.Log("💡 VibrateShort はこのプラットフォームでは未対応");
#endif
    }
    public void SetVibration(bool enabled) => vibrationEnabled = enabled;

    public bool IsVibrationEnabled() => vibrationEnabled;
}
