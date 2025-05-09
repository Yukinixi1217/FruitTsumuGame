using UnityEngine;

public class ShowRewardedAd : MonoBehaviour
{
    public void ShowAd()
    {
        Debug.Log("📢 仮の広告表示中（実広告なし）");

        // 本来は広告が成功したときの報酬処理
        RewardUser();
    }

    void RewardUser()
    {
        Debug.Log("💰 ユーザーに仮報酬（+10コイン）を付与");
        CoinManager.Instance.AddCoin(10);
    }
}
