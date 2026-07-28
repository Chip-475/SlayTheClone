using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using DG.Tweening;

public class MM_StartButton : MonoBehaviour
{
    public GameObject panel;

    public void OnStartClick()
    {
        panel.SetActive(true);
    }
    public async void OnYesClick()
    {
        var panel = MM_Manager.instance.fadePanel;
        await panel.GetComponent<CanvasGroup>().DOFade(1, 0.3f)
            .AsyncWaitForCompletion();
        await SceneManager.LoadSceneAsync("Map", LoadSceneMode.Single);
    }
    public void OnNoClick()
    {
        panel.SetActive(false);
    }
}
