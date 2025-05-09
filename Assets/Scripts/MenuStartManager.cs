using UnityEngine;

public class MenuStartManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;

    void Start()
    {
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
