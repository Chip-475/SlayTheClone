using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;

public class MM_SettingsMenu : MonoBehaviour
{
    #region Declarations
    public Image settingsMenu;
    public bool menuOpen;
    #endregion

    private Slider masterSlider;
    private Slider sfxSlider;
    private Slider bgmSlider;

    private void Awake()
    {
        if (settingsMenu == null)
        {
            return;
        }

        foreach (Slider slider in settingsMenu.GetComponentsInChildren<Slider>(true))
        {
            switch (slider.gameObject.name)
            {
                case "Master Slider":
                    masterSlider = slider;
                    break;
                case "SFX Slider":
                    sfxSlider = slider;
                    break;
                case "BGM Slider":
                    bgmSlider = slider;
                    break;
            }
        }
    }

    private void Start()
    {
        InitializeSlider(masterSlider, AudioManager.MasterVolumeKey);
        InitializeSlider(sfxSlider, AudioManager.SFXVolumeKey);
        InitializeSlider(bgmSlider, AudioManager.BGMVolumeKey);
        AudioManager.ApplySavedVolumes();
    }

    private void InitializeSlider(Slider slider, string key)
    {
        if (slider == null)
        {
            return;
        }

        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.SetValueWithoutNotify(AudioManager.GetSavedVolume(key));
    }

    #region Methods
    public void ToggleMenu()
    {
        if(!menuOpen)
        {
            // Open menu

            settingsMenu.transform.DOScale(1, 0.15f);
            menuOpen = true;
        }
        else
        {
            // Close menu

            settingsMenu.transform.DOScale(0, 0.15f);
            menuOpen = false;
        }
    }

    public void SetMaster(float volume)
    {
        SetVolume(AudioManager.MasterVolumeKey, "Master", volume);
    }
    public void SetSFX(float volume)
    {
        SetVolume(AudioManager.SFXVolumeKey, "SFX", volume);
    }
    public void SetBGM(float volume)
    {
        SetVolume(AudioManager.BGMVolumeKey, "BGM", volume);
    }

    private void SetVolume(string key, string parameter, float volume)
    {
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
        AudioManager.SetMixerVolume(parameter, volume);
    }
    #endregion
}
