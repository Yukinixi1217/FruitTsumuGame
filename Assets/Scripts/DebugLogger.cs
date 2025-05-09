using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DebugLogger : MonoBehaviour
{
    public Text debugText; // UI の Text コンポーネントをセット
    private Queue<string> logQueue = new Queue<string>();
    private const int maxLines = 7; // 最大表示行数

    private void Awake()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void Start()
    {
        // シーンロード時に UI を取得
        if (debugText == null)
        {
            debugText = GameObject.Find("DebugText")?.GetComponent<Text>();

            if (debugText == null)
            {
                Debug.LogWarning("DebugText が見つかりません！");
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logQueue.Count >= maxLines)
        {
            logQueue.Dequeue(); // 古い行を削除
        }
        logQueue.Enqueue(logString);

        if (debugText != null)
        {
            debugText.text = string.Join("\n", logQueue.ToArray());
        }
        else
        {
            Debug.LogWarning("DebugText の参照が切れています！");
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog; // リスナーを解除
    }
}
