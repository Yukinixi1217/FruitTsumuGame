using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static UnityAdsManager Instance { get; private set; }

    [SerializeField] private string androidGameId = "1486550";
    [SerializeField] private string adUnitId = "rewardedVideo";
    [SerializeField] private bool testMode = true;

    [SerializeField] private RewardPopupController rewardPopup;

    private bool adLoaded = false;

    private void Awake()
    {
#if UNITY_EDITOR
        testMode = true;
#else
        testMode = false;
#endif
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (rewardPopup == null)
        {
            rewardPopup = FindAnyObjectByType<RewardPopupController>();
            if (rewardPopup == null)
            {
                Debug.LogError("⚠️ RewardPopupController がシーン内に見つかりません！");
            }
        }
    }

    private void Start()
    {
        Debug.Log("▶ UnityAdsManager.Start() 呼び出し開始");
        Advertisement.Initialize(androidGameId, testMode, this);
    }

    public void ShowAd()
    {
        Debug.Log("✅ ShowAd() が呼ばれました");

        if (adLoaded)
        {
            Debug.Log("▶ 広告を表示します");
            Advertisement.Show(adUnitId, this);
        }
        else
        {
            Debug.LogWarning("❌ 広告がまだ読み込まれていません");
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("✅ Unity Ads 初期化完了");
        Advertisement.Load(adUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError("❌ 初期化失敗: " + error + " - " + message);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == adUnitId)
        {
            Debug.Log("✅ 広告読み込み完了");
            adLoaded = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError("❌ 広告ロード失敗: " + error + " - " + message);
        adLoaded = false;
    }

    private int CalculateReward()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogWarning("⚠ LevelManager.Instance が null のため、デフォルト報酬（+50）を適用します。");
            return 50;
        }

        int score = LevelManager.Instance.CurrentScore;

        if (score >= 2000) return 200;
        if (score >= 1000) return 100;
        return 50;
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("🟨 OnUnityAdsShowComplete 呼び出し");

        if (placementId == adUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            try
            {
                int rewardAmount = CalculateReward();
                CoinManager.Instance?.AddCoin(rewardAmount);

                if (rewardPopup != null)
                {
                    Debug.Log($"[DEBUG] ポップアップ表示: +{rewardAmount}");
                    rewardPopup.Show(rewardAmount);
                }
                else
                {
                    Debug.LogError("❌ rewardPopup が null！Inspectorで設定されていません！");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("❌ 広告終了後の処理で例外: " + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError("❌ 広告表示失敗: " + error + " - " + message);
    }

    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
}
