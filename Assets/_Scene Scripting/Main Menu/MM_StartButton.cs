using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class MM_StartButton : MonoBehaviour
{
    public GameObject panel;
    public bool ngPressed;
    public TMP_Text text;

    public void OnStartClick()
    {
        panel.SetActive(true);
        if (ngPressed) text.text = "Start a new game?";
        else text.text = "Load last game?";
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
