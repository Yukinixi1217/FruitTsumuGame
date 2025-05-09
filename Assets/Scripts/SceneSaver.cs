using UnityEngine;
using UnityEngine.SceneManagement;

public static class test_SceneSaver
{
    private const string SceneKey = "SavedScene";

    public static void SaveCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(SceneKey, currentScene);
        PlayerPrefs.Save();
        Debug.Log($"シーン {currentScene} を保存しました！");
    }

    public static void LoadSavedScene()
    {
        if (PlayerPrefs.HasKey(SceneKey))
        {
            string sceneToLoad = PlayerPrefs.GetString(SceneKey);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("保存されたシーンがありません。");
        }
    }
}
