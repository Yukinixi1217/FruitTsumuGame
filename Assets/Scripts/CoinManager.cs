// CoinManager.cs に以下のような自動 CoinText 検出機能を追加します

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    private int currentCoins = 0;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadCoins();
    }

    private void Start()
    {
        FindCoinTextIfNull();
        UpdateCoinText(); // セーブ値をTextに反映
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void FindCoinTextIfNull()
    {
        if (coinText == null)
        {
            GameObject obj = GameObject.FindWithTag("CoinText");
            if (obj != null)
            {
                coinText = obj.GetComponent<TextMeshProUGUI>();
            }
            if (coinText == null)
            {
                Debug.LogWarning("[CoinManager] CoinText が見つかりませんでした");
            }
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        coinText = GameObject.Find("CoinText")?.GetComponent<TextMeshProUGUI>();
        UpdateCoinText(); // セーブ値をTextに反映
    }

    private void TryAutoFindCoinText()
    {
        if (coinText == null)
        {
            coinText = GameObject.Find("CoinText")?.GetComponent<TextMeshProUGUI>();
            if (coinText != null)
            {
                Debug.Log("✅ CoinText を自動的に設定しました: " + coinText.name);
            }
            else
            {
                Debug.LogWarning("⚠️ CoinText が見つかりませんでした。自動設定失敗。");
            }
        }
    }

    public void SetCoinTextTarget(TextMeshProUGUI target)
    {
        coinText = target;
        UpdateCoinText();
    }

    public void AddCoin(int amount)
    {
        currentCoins += amount;
        SaveCoins();
        UpdateCoinText();
    }

    public void UseCoin(int amount)
    {
        currentCoins = Mathf.Max(0, currentCoins - amount);
        SaveCoins();
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = currentCoins.ToString();
        }
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", currentCoins);
    }

    private void LoadCoins()
    {
        currentCoins = PlayerPrefs.GetInt("Coins", 0);
    }
}
