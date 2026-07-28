using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class RestManager : MonoBehaviour
{
    #region Declarations
    public static RestManager instance;
    public MainDatabase Database => MainDatabase.instance;

    public CraftMenu craftMenu;

    public int time;
    public GameObject fadePanel;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;

        craftMenu = GetComponent<CraftMenu>();
    }
    private void Start()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
    }
    #endregion

    #region Methods
    public static void DecreaseTime(int amount)
    {
        instance.time -= amount;
    }

    public async void OnLeaveClick()
    {
        await fadePanel.GetComponent<CanvasGroup>().DOFade(1, 0.3f)
            .AsyncWaitForCompletion();
        await SceneManager.LoadSceneAsync("Map", LoadSceneMode.Single);
    }
    #endregion
}
