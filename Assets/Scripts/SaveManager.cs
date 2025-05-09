using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public bool IsLoaded { get; private set; } = false;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    private SaveData saveData = new SaveData(); // ← Null防止に初期化しておくのもGOOD
    private string savePath;

    public static SaveManager Instance { get; private set; }
    private const string AdsKey = "AdsEnabled";
    private const string VibrateKey = "VibrateEnabled";
    private const string SoundKey = "SoundEnabled";
    private const string MusicKey = "MusicEnabled";

    public static bool IsMusicEnabled()
    {
        return PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
    }

    public static bool IsSoundEnabled()
    {
        return PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
    }

    public static bool IsVibrateEnabled()
    {
        return PlayerPrefs.GetInt("VibrateEnabled", 1) == 1;
    }

    public static bool IsAdsEnabled()
    {
        return PlayerPrefs.GetInt("AdsEnabled", 1) == 1;
    }

    public static void SaveMusicEnabled(bool enabled)
    {
        PlayerPrefs.SetInt(MusicKey, enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool LoadMusicEnabled()
    {
        return PlayerPrefs.GetInt(MusicKey, 1) == 1;  // 初期値: ON（true）
    }

    public static void SaveSoundEnabled(bool enabled)
    {
        PlayerPrefs.SetInt(SoundKey, enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool LoadSoundEnabled()
    {
        return PlayerPrefs.GetInt(SoundKey, 1) == 1;  // 初期状態は ON
    }

    public static void SaveVibrateEnabled(bool enabled)
    {
        PlayerPrefs.SetInt(VibrateKey, enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool LoadVibrateEnabled()
    {
        return PlayerPrefs.GetInt(VibrateKey, 1) == 1; // 初期値: ON（true）
    }

    public static void SaveAdsEnabled(bool enabled)
    {
        PlayerPrefs.SetInt(AdsKey, enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool LoadAdsEnabled()
    {
        return PlayerPrefs.GetInt(AdsKey, 1) == 1; // 初期値は有効（1）
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    private void Start()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    public void SaveGame()
    {
        if (float.TryParse(timerText.text, out float timerValue))
            saveData.timer = timerValue;

        if (int.TryParse(scoreText.text, out int scoreValue))
            saveData.score = scoreValue;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);

        //Debug.Log("ゲームデータをセーブしました！: " + json);
    }

    private IEnumerator LoadGameCoroutine()
    {
        yield return null;

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);

            //Debug.Log("ゲームデータをロードしました！: " + json);
            //Debug.Log("ロードした timer: " + saveData.timer);
            //Debug.Log("ロードした score: " + saveData.score);

            if (timerText != null)
                timerText.text = ((int)saveData.timer).ToString();

            if (scoreText != null)
                scoreText.text = saveData.score.ToString();

            //Debug.Log("表示した timer: " + timerText.text);
            //Debug.Log("表示した score: " + scoreText.text);
            IsLoaded = true;
        }
        else
        {
            //Debug.Log("セーブデータが見つかりません。新しいデータを作成します。");
        }
    }

    // 👇ここに正しく入れる！
    public float GetSavedTime()
    {
        return saveData.timer;
    }

    public int GetSavedScore()
    {
        return saveData.score;
    }
}

[System.Serializable]
public class SaveData
{
    public float timer;
    public int score;
}
