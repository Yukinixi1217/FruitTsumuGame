using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    private int coin = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        Debug.Log($"🪙 コイン {amount} 枚追加されました（合計: {coin}）");
    }

    public int GetCoin()
    {
        return coin;
    }
}
