using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    public void ResumeGame()
    {
        PlayerPrefs.SetString("launchMode", "resume");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        PlayerPrefs.SetString("launchMode", "restart");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}
