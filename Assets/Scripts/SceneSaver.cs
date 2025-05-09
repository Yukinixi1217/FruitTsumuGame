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
        Debug.Log($"�V�[�� {currentScene} ��ۑ����܂����I");
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
            Debug.Log("�ۑ����ꂽ�V�[��������܂���B");
        }
    }
}
