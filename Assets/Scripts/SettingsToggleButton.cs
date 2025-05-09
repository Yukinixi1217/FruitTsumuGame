// ----------------------------
// Settings�֘A�X�N���v�g
// ----------------------------
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// ----------------------------
// SettingsToggleButton.cs
// ----------------------------
// �e�ݒ�{�^���i���y�A�T�E���h�A�o�C�u�A�L���j�̃g�O����Ԃ��Ǘ�
// ON/OFF�A�C�R���̐؂�ւ��� PlayerPrefs �ւ̕ۑ����s��
public class SettingsToggleButton : MonoBehaviour, IPointerClickHandler
{
    [Header("PlayerPrefs�p�L�[")]
    public string toggleKey = "Settings_Music";

    [Header("������ԁiON�j")]
    public bool defaultIsOn = true;

    [Header("�A�C�R������")]
    public GameObject iconOnObject;   // ON��Ԃ̂Ƃ��\������A�C�R��
    public GameObject iconOffObject;  // OFF��Ԃ̂Ƃ��\������A�C�R��

    [Header("ON/OFF�؂�ւ��C�x���g")]
    public UnityEvent<bool> onToggleChanged; // ��ԕύX���ɌĂяo���C�x���g

    private bool isOn; // ���݂̏�ԁitrue = ON, false = OFF�j

    private void Awake()
    {
        // �ۑ�����Ă���ݒ��ǂݍ��݁i�Ȃ���΃f�t�H���g�j
        isOn = PlayerPrefs.GetInt(toggleKey, defaultIsOn ? 1 : 0) == 1;
        ApplyState();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle(); // �{�^���������ꂽ�Ƃ��ɏ�Ԃ�؂�ւ���
    }

    public void Toggle()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt(toggleKey, isOn ? 1 : 0); // �ۑ�
        PlayerPrefs.Save();

        ApplyState();
        onToggleChanged?.Invoke(isOn); // �C�x���g�Ăяo���iAudioManager�Ȃǁj
    }

    public void ApplyState()
    {
        // �A�C�R����ON/OFF��؂�ւ���
        if (iconOnObject != null) iconOnObject.SetActive(isOn);
        if (iconOffObject != null) iconOffObject.SetActive(!isOn);
    }
}

// ----------------------------
// SettingsPanelManager.cs
// ----------------------------
// �N�����ɑS�g�O���{�^���̏�Ԃ𕜌�����
public class SettingsPanelManager : MonoBehaviour
{
    [Header("�Ώۃg�O���{�^���ꗗ")]
    public SettingsToggleButton[] toggleButtons; // �SUI_MusicButton �Ȃ�

    private void Start()
    {
        // �S�Ẵg�O���{�^���ɑ΂��ď�����Ԃ�K�p
        foreach (var toggle in toggleButtons)
        {
            if (toggle != null)
                toggle.ApplyState();
        }
    }
}