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
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Methods
    public static void PlaySFX(AudioClip clip, Transform spawn, float volume)
    {
        AudioSource audioSource = Instantiate(instance.source, spawn.position, Quaternion.identity); ;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    public static void PlaySFX(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource audioSource = Instantiate(instance.source, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    #endregion

}