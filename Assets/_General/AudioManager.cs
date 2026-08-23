using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    #region Declarations
    public static AudioManager instance;

    public const string MasterVolumeKey = "Master Volume";
    public const string SFXVolumeKey = "SFX Volume";
    public const string BGMVolumeKey = "BGM Volume";
    public const float DefaultVolume = 1f;

    public AudioSource source;
    public AudioMixer mixer;
    public AudioMixerGroup sfxMixerGroup;

    public AudioSource mainThemePlayer;
    public AudioSource battleThemePlayer;
    #endregion

    #region Unity Methods
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (sfxMixerGroup == null && mixer != null)
        {
            AudioMixerGroup[] groups = mixer.FindMatchingGroups("SFX");
            if (groups.Length > 0)
            {
                sfxMixerGroup = groups[0];
            }
        }

        ApplySavedVolumes();
    }
    private void Start()
    {
        // The main-menu scene historically left this field empty. The SFX source
        // prefab is also configured with the main theme clip, so it is a safe
        // fallback when the dedicated reference is missing.
        if (mainThemePlayer == null)
        {
            mainThemePlayer = source;
        }

        if (mainThemePlayer == null)
        {
            Debug.LogError("AudioManager has no audio source prefab assigned.");
            return;
        }

        mainThemePlayer = Instantiate(mainThemePlayer, transform);
        mainThemePlayer.loop = true;
        mainThemePlayer.volume = 0f;
        mainThemePlayer.Play();
        FadeVolume(mainThemePlayer, 1f);

        battleThemePlayer = Instantiate(battleThemePlayer, transform);
        battleThemePlayer.loop = true;
        battleThemePlayer.volume = 0f;
        battleThemePlayer.Pause();
        FadeVolume(battleThemePlayer, 1f);
    }
    #endregion

    #region Methods
    public static void PlaySFX(AudioClip clip, Transform spawn, float volume)
    {
        if (instance == null || instance.source == null || clip == null || spawn == null)
        {
            return;
        }

        AudioSource audioSource = Instantiate(instance.source, spawn.position, Quaternion.identity); ;
        audioSource.outputAudioMixerGroup = instance.sfxMixerGroup;
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    public static void PlaySFX(AudioClip clip, Vector3 position, float volume)
    {
        if (instance == null || instance.source == null || clip == null)
        {
            return;
        }

        AudioSource audioSource = Instantiate(instance.source, position, Quaternion.identity);
        audioSource.outputAudioMixerGroup = instance.sfxMixerGroup;
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public static float GetSavedVolume(string key)
    {
        return Mathf.Clamp01(PlayerPrefs.GetFloat(key, DefaultVolume));
    }

    public static void ApplySavedVolumes()
    {
        SetMixerVolume("Master", GetSavedVolume(MasterVolumeKey));
        SetMixerVolume("SFX", GetSavedVolume(SFXVolumeKey));
        SetMixerVolume("BGM", GetSavedVolume(BGMVolumeKey));
    }

    public static void SetMixerVolume(string parameter, float volume)
    {
        if (instance == null || instance.mixer == null)
        {
            return;
        }

        volume = Mathf.Clamp01(volume);
        instance.mixer.SetFloat(parameter, volume <= 0f ? -80f : Mathf.Log10(volume) * 20f);
    }

    public static void FadeVolume(AudioSource source, float endVolume)
    {
        if (source == null)
        {
            return;
        }

        source.DOKill();
        DOTween.To(() => source.volume, volume => source.volume = volume,
            Mathf.Clamp01(endVolume), 0.2f);
    }
    #endregion

}
