using UnityEngine;
using System.IO;

[System.Serializable]
public class SettingData
{
    public bool musicOn = true;
    public bool soundOn = true;
    public bool vibrateOn = true;
    public bool adsOn = true;
}

public class SettingSaveManager : MonoBehaviour
{
    public static SettingSaveManager Instance { get; private set; }

    private const string SavePath = "settings.json";
    public SettingData Data { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, SavePath);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data = JsonUtility.FromJson<SettingData>(json);
        }
        else
        {
            Data = new SettingData();
            Save();
        }
    }

    public void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, SavePath);
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(path, json);
    }

    public void SetMusic(bool on)
    {
        Data.musicOn = on;
        Save();
        AudioManager.Instance?.SetMusicMute(!on);
    }

    public void SetSound(bool on)
    {
        Data.soundOn = on;
        Save();
    }

    public void SetVibrate(bool on)
    {
        Data.vibrateOn = on;
        Save();
    }

    public void SetAds(bool on)
    {
        Data.adsOn = on;
        Save();
    }
}
