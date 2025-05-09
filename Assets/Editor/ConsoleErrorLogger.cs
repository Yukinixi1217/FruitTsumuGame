using UnityEditor;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public static class ConsoleErrorLogger
{
    private static readonly string logFilePath = "BuildErrorsOnly.txt";

    static ConsoleErrorLogger()
    {
        Application.logMessageReceived += HandleLog;
    }

    private static void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            try
            {
                File.AppendAllText(logFilePath,
                    $"[{type}] {logString}\n{stackTrace}\n-----------------------------\n");
            }
            catch (IOException e)
            {
                Debug.LogWarning("ログファイルへの書き込みに失敗: " + e.Message);
            }
        }
    }
}
