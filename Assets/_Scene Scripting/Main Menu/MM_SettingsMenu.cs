using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MM_SettingsMenu : MonoBehaviour
{
    public Image settingsMenu;
    public bool menuOpen;

    public void OnClick()
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
}
