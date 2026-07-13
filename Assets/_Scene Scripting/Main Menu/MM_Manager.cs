using UnityEngine;

public class MM_Manager : MonoBehaviour
{
    #region Declarations
    public static MM_Manager instance;

    public MM_SettingsMenu settingsMenu;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        settingsMenu = GetComponent<MM_SettingsMenu>();
    }
    #endregion

    #region Methods
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
}
