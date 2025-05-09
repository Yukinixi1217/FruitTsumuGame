using UnityEngine;
using UnityEngine.UI;

public class SettingsToggleController : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle vibrateToggle; // Å© Ç±ÇÍÇ™ïKóv
    [SerializeField] private Toggle adsToggle;

    [Header("UI Buttons")]
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button vibrateButton;
    [SerializeField] private Button adsButton;

    [Header("Icons")]
    [SerializeField] private GameObject iconMusicOn;
    [SerializeField] private GameObject iconMusicOff;
    [SerializeField] private GameObject iconSoundOn;
    [SerializeField] private GameObject iconSoundOff;
    [SerializeField] private GameObject iconVibrateOn;
    [SerializeField] private GameObject iconVibrateOff;
    [SerializeField] private GameObject iconAdsOn;
    [SerializeField] private GameObject iconAdsOff;

    private void Start()
    {
        LoadAndApply();

        musicButton.onClick.AddListener(OnMusicToggleClick);
        soundButton.onClick.AddListener(OnSoundToggleClick);
        vibrateButton.onClick.AddListener(OnVibrateToggleClick);
        adsButton.onClick.AddListener(OnAdsToggleClick);
    }

    private void LoadAndApply()
    {
        bool musicOn = SaveManager.LoadMusicEnabled();
        bool soundOn = SaveManager.LoadSoundEnabled();
        bool vibrateOn = SaveManager.LoadVibrateEnabled();
        bool adsOn = SaveManager.LoadAdsEnabled();

        ApplyIcon(iconMusicOn, iconMusicOff, musicOn);
        ApplyIcon(iconSoundOn, iconSoundOff, soundOn);
        ApplyIcon(iconVibrateOn, iconVibrateOff, vibrateOn);
        ApplyIcon(iconAdsOn, iconAdsOff, adsOn);

        AudioManager.Instance?.SetMusicMute(!musicOn);
        AudioManager.Instance?.SetSoundMute(!soundOn);
    }

    private void ApplyIcon(GameObject onIcon, GameObject offIcon, bool enabled)
    {
        if (onIcon != null) onIcon.SetActive(enabled);
        if (offIcon != null) offIcon.SetActive(!enabled);
    }

    private void OnMusicToggleClick()
    {
        bool current = SaveManager.LoadMusicEnabled();
        bool next = !current;
        SaveManager.SaveMusicEnabled(next);
        ApplyIcon(iconMusicOn, iconMusicOff, next);
        AudioManager.Instance?.SetMusicMute(!next);
    }

    private void OnSoundToggleClick()
    {
        bool current = SaveManager.LoadSoundEnabled();
        bool next = !current;
        SaveManager.SaveSoundEnabled(next);
        ApplyIcon(iconSoundOn, iconSoundOff, next);
        AudioManager.Instance?.SetSoundMute(!next);
    }

    private void OnVibrateToggleClick()
    {
        bool current = SaveManager.LoadVibrateEnabled();
        bool next = !current;
        SaveManager.SaveVibrateEnabled(next);
        ApplyIcon(iconVibrateOn, iconVibrateOff, next);
    }

    private void OnAdsToggleClick()
    {
        bool current = SaveManager.LoadAdsEnabled();
        bool next = !current;
        SaveManager.SaveAdsEnabled(next);
        ApplyIcon(iconAdsOn, iconAdsOff, next);
    }
    public void OnVibrateToggle()
    {
        bool isEnabled = vibrateToggle.isOn;
        SaveManager.SaveVibrateEnabled(isEnabled); // âië±ï€ë∂

#if UNITY_ANDROID && !UNITY_EDITOR
    if (isEnabled)
    {
        VibrationManager.Instance.VibrateShort(); // ämîFópÇ…åyÇ≠êUìÆ
    }
#endif
    }

}
