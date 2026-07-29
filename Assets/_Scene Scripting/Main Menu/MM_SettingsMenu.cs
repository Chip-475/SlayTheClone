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
        PlayerPrefs.SetFloat("Master Volume", volume);
        SetMixerVolume("Master", volume);
    }
    public void SetSFX(float volume)
    {
        PlayerPrefs.SetFloat("SFX Volume", volume);
        SetMixerVolume("SFX", volume);
    }
    public void SetBGM(float volume)
    {
        PlayerPrefs.SetFloat("BGM Volume", volume);
        SetMixerVolume("BGM", volume);
    }

    private void SetMixerVolume(string parameter, float volume)
    {
        if (volume <= 0f)
        {
            AudioManager.instance.mixer.SetFloat(parameter, -80f);
            return;
        }

        AudioManager.instance.mixer.SetFloat(parameter, Mathf.Log10(volume) * 20);
    }
    #endregion
}
