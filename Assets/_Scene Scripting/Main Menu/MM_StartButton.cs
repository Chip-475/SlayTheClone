using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using DG.Tweening;

public class MM_StartButton : MonoBehaviour
{
    public GameObject panel;
    public bool ngPressed;

    public void OnStartClick()
    {
        panel.SetActive(true);
    }
    public void OnNewGameClick()
    {
       ngPressed = true;
        OnStartClick();
    }
    public async void OnYesClick()
    {
        if (ngPressed) { Database.newFile = true; }

        var panel = MM_Manager.instance.fadePanel;
        await panel.GetComponent<CanvasGroup>().DOFade(1, 0.3f)
            .AsyncWaitForCompletion();
        await SceneManager.LoadSceneAsync("Map", LoadSceneMode.Single);
    }
    public void OnNoClick()
    {
        panel.SetActive(false);
        ngPressed = false;
    }
}
