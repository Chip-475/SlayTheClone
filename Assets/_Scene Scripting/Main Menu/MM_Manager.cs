using DG.Tweening;
using UnityEngine;

public class MM_Manager : MonoBehaviour
{
    #region Declarations
    public static MM_Manager instance;

    public MM_SettingsMenu settingsMenu;

    public GameObject fadePanel;
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
    private void Start()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
    }
    #endregion

    #region Methods
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
}
