// ----------------------------
// Settings関連スクリプト
// ----------------------------
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// ----------------------------
// SettingsToggleButton.cs
// ----------------------------
// 各設定ボタン（音楽、サウンド、バイブ、広告）のトグル状態を管理
// ON/OFFアイコンの切り替えや PlayerPrefs への保存も行う
public class SettingsToggleButton : MonoBehaviour, IPointerClickHandler
{
    [Header("PlayerPrefs用キー")]
    public string toggleKey = "Settings_Music";

    [Header("初期状態（ON）")]
    public bool defaultIsOn = true;

    [Header("アイコン制御")]
    public GameObject iconOnObject;   // ON状態のとき表示するアイコン
    public GameObject iconOffObject;  // OFF状態のとき表示するアイコン

    [Header("ON/OFF切り替えイベント")]
    public UnityEvent<bool> onToggleChanged; // 状態変更時に呼び出すイベント

    private bool isOn; // 現在の状態（true = ON, false = OFF）

    private void Awake()
    {
        // 保存されている設定を読み込み（なければデフォルト）
        isOn = PlayerPrefs.GetInt(toggleKey, defaultIsOn ? 1 : 0) == 1;
        ApplyState();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle(); // ボタンが押されたときに状態を切り替える
    }

    public void Toggle()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt(toggleKey, isOn ? 1 : 0); // 保存
        PlayerPrefs.Save();

        ApplyState();
        onToggleChanged?.Invoke(isOn); // イベント呼び出し（AudioManagerなど）
    }

    public void ApplyState()
    {
        // アイコンのON/OFFを切り替える
        if (iconOnObject != null) iconOnObject.SetActive(isOn);
        if (iconOffObject != null) iconOffObject.SetActive(!isOn);
    }
}

// ----------------------------
// SettingsPanelManager.cs
// ----------------------------
// 起動時に全トグルボタンの状態を復元する
public class SettingsPanelManager : MonoBehaviour
{
    [Header("対象トグルボタン一覧")]
    public SettingsToggleButton[] toggleButtons; // 全UI_MusicButton など

    private void Start()
    {
        // 全てのトグルボタンに対して初期状態を適用
        foreach (var toggle in toggleButtons)
        {
            if (toggle != null)
                toggle.ApplyState();
        }
    }
}