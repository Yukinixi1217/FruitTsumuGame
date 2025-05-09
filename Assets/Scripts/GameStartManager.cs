using UnityEngine;
using TMPro;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private AudioClip backgroundMusic;
    void aStart()
    {
        AudioSource bgmSource = AudioManager.Instance.bgmAudioSource;
        bgmSource.clip = AudioManager.Instance.bgmClip; // 既にInspectorに設定されているClip
        bgmSource.loop = true;
        bgmSource.Play();

        Debug.Log("テスト再生: " + (bgmSource.clip != null ? bgmSource.clip.name : "clipがnull"));

        // いつもの初期処理も残してOK
        string mode = PlayerPrefs.GetString("launchMode", "restart");
        if (mode == "resume") LoadSavedState();
        else StartNewGame();
    }

    void Start()
    {
        EnsureVibrationManager();
        //AudioManager.Instance.PlayBgm(); // ✅ 引数なしに修正
        AudioSource bgmSource = AudioManager.Instance.bgmAudioSource;
        AudioManager.Instance.PlayBgm(AudioManager.Instance.bgmClip);
        Debug.Log("GameStartManagerで再生するBGM: " + (AudioManager.Instance.bgmClip != null ? AudioManager.Instance.bgmClip.name : "null"));
        bgmSource.loop = true;
        bgmSource.mute = false;

        string mode = PlayerPrefs.GetString("launchMode", "restart");

        if (mode == "resume")
        {
            Debug.Log("Resuming game...");
            LoadSavedState();
        }
        else
        {
            Debug.Log("Starting new game...");
            StartNewGame();
        }

    }
    private void EnsureVibrationManager()
    {
        if (VibrationManager.Instance == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/VibrationManager");
            if (prefab != null)
            {
                GameObject instance = Instantiate(prefab);
                DontDestroyOnLoad(instance);
                Debug.Log("✅ VibrationManager 自動生成 & 永続化");
            }
            else
            {
                Debug.LogError("❌ Resources/VibrationManager.prefab が見つかりません！");
            }
        }
        else
        {
            Debug.Log("✅ VibrationManager はすでに存在しています");
        }
    }

    void LoadSavedState()
    {
        if (SaveManager.Instance != null)
        {
            int score = SaveManager.Instance.GetSavedScore();
            float time = SaveManager.Instance.GetSavedTime();

            if (scoreText != null) scoreText.text = score.ToString();
            if (timerText != null) timerText.text = time.ToString("F1");
        }
    }

    void StartNewGame()
    {
        if (scoreText != null) scoreText.text = "0";
        if (timerText != null) timerText.text = "60.0";
    }
}
