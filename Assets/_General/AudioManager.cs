using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

    public static AudioManager instance;

    public AudioSource source;
    public AudioMixer mixer;
    #endregion

    #region Unity Methods
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        
    }
    #endregion

    #region Methods
    public void PlaySFX(AudioClip clip, Transform spawn, float volume)
    {
        AudioSource audioSource = Instantiate(source, spawn.position, Quaternion.identity); ;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    public void PlaySFX(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource audioSource = Instantiate(source, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void SetMaster(float volume)
    {
        //data.master = volume;
        PlayerPrefs.SetFloat("master", volume);
        ApplyMixerVolume("Master", volume);
    }
    public void SetSFX(float volume)
    {
        //data.sfx = volume;
        PlayerPrefs.SetFloat("sfx", volume);
        ApplyMixerVolume("SFX", volume);
    }
    public void SetBGM(float volume)
    {
        //data.music = volume;
        PlayerPrefs.SetFloat("music", volume);
        ApplyMixerVolume("BGM", volume);
    }

    private void ApplyMixerVolume(string parameter, float volume)
    {
        if (volume <= 0f)
        {
            mixer.SetFloat(parameter, -80f);
            return;
        }

        mixer.SetFloat(parameter, Mathf.Log10(volume) * 20);
    }
    #endregion

}