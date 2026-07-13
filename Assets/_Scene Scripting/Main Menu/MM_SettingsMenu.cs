using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;

public class MM_SettingsMenu : MonoBehaviour
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

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

            SaveAndLoad.instance.SaveSettings();
            settingsMenu.transform.DOScale(0, 0.15f);
            menuOpen = false;
        }
    }

    public void SetMaster(float volume)
    {
        Database.settings.masterVolume = volume;
        SetMixerVolume("Master", volume);
    }
    public void SetSFX(float volume)
    {
        Database.settings.sfxVolume = volume;
        SetMixerVolume("SFX", volume);
    }
    public void SetBGM(float volume)
    {
        Database.settings.bgmVolume = volume;
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
