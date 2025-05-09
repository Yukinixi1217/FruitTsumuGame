using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    [Header("Audio Clips")]
    public AudioClip bgmClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("AudioListener.volume = " + AudioListener.volume);

        // もし 0 だったら強制的に 1 にする
        if (AudioListener.volume <= 0f)
        {
            Debug.LogWarning("AudioListener.volume is 0. Resetting to 1.");
            AudioListener.volume = 1f;
        }
        if (bgmAudioSource != null && bgmClip != null && SaveManager.IsMusicEnabled())
        {
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
            Debug.Log("[AudioManager] BGM再生開始");
            
        }
    }

    public void SetMusicMute(bool isMuted)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.mute = isMuted;
        }
    }

    public void SetSoundMute(bool isMuted)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.mute = isMuted;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null || sfxAudioSource == null || !SaveManager.IsSoundEnabled())
            return;

        sfxAudioSource.PlayOneShot(clip);

        if (VibrationManager.Instance != null && SaveManager.IsVibrateEnabled())
        {
            VibrationManager.Instance.VibrateShort();
        }
    }

    public void PlayBgm(AudioClip clip)
    {
        if (bgmAudioSource == null)
        {
            Debug.LogError("BGM AudioSource が未設定です！");
            return;
        }

        if (clip == null)
        {
            Debug.LogError("BGM Clip が設定されていません！");
            return;
        }

        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;

        // 応急対応（強制ON）
        bgmAudioSource.mute = false;
        bgmAudioSource.volume = 1f;

        bgmAudioSource.Play();
        Debug.Log("✅ BGM再生開始: " + clip.name);
    }
    
}
