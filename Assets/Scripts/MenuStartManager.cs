using UnityEngine;

public class MenuStartManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;

    void Start()
    {
        // BGM設定を強制ONにする（開発時のみ）
        //PlayerPrefs.SetInt("Settings_Music", 1);
        //PlayerPrefs.Save();
        //Debug.Log("[Init] Settings_Music を 1（ON）に初期化しました");

        EnsureVibrationManager();

        // BGM 再生（必要なら）
        if (backgroundMusic != null)
        {
            AudioManager.Instance?.PlayBgm(backgroundMusic);
            Debug.Log("MenuStartManagerで再生するBGM: " + backgroundMusic.name);
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
                Debug.Log("✅ VibrationManager を MenuScene に生成しました");
            }
            else
            {
                Debug.LogWarning("⚠️ VibrationManager のプレハブが Resources/Prefabs に存在しません！");
            }
        }
        else
        {
            Debug.Log("✔️ VibrationManager は既に存在しています");
        }
    }
}
