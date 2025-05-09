using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DebugLogger : MonoBehaviour
{
    public Text debugText; // UI �� Text �R���|�[�l���g���Z�b�g
    private Queue<string> logQueue = new Queue<string>();
    private const int maxLines = 7; // �ő�\���s��

    private void Awake()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void Start()
    {
        // �V�[�����[�h���� UI ���擾
        if (debugText == null)
        {
            debugText = GameObject.Find("DebugText")?.GetComponent<Text>();

            if (debugText == null)
            {
                Debug.LogWarning("DebugText ��������܂���I");
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logQueue.Count >= maxLines)
        {
            logQueue.Dequeue(); // �Â��s���폜
        }
        logQueue.Enqueue(logString);

        if (debugText != null)
        {
            debugText.text = string.Join("\n", logQueue.ToArray());
        }
        else
        {
            Debug.LogWarning("DebugText �̎Q�Ƃ��؂�Ă��܂��I");
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog; // ���X�i�[������
    }
}
