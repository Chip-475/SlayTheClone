using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Threading.Tasks;
using static MapManager;
using UnityEngine.LightTransport;

/// <summary>
/// EST => Equipped Skills Tab
/// UST => Unlocked Skills Tab
/// </summary>
public class LoadoutMenuManager : MonoBehaviour
{
    public GameObject loadoutPanel;
    public EventTrigger loadout_open;
    public EventTrigger loadout_close;

    #region Loadout Panel
    [Header("Resources")]
    public SlotUI skillSlot;

    [Header("Equipped Skills Tab")]
    public GameObject est;
    public Transform est_inner;
    public Transform est_outer;

    [Header("Unlocked Skills Tab")]
    public GameObject ust;
    public Transform ust_inner;
    public Transform ust_outer;
    [Space]
    public Transform ust_content;
    #endregion

    private void Awake()
    {
        if(loadoutPanel.activeSelf) menuHistory.Push(loadoutPanel);
    }
    void Start()
    {
        loadoutPanel.SetActive(false);

        loadout_open.AddEvent
        (
            EventTriggerType.PointerClick,
            async action => { // Function order is important.
                loadoutPanel.SetActive(true);
                menuHistory.Push(loadoutPanel);
                loadout_open.gameObject.SetActive(false);
                await OpenLoadoutMenu();
            }
        );
        loadout_close.AddEvent
        (
            EventTriggerType.PointerClick,
            async action => { // Function order is important.
                loadout_open.gameObject.SetActive(true);
                await CloseLoadoutMenu();
                loadoutPanel.SetActive(false);
                menuHistory.Pop(); 
            }
        );
    }

    void BuildUST(int nSlots)
    {
        foreach (Transform child in ust_content) Destroy(child.gameObject);

        for (int i = 0; i < nSlots; i++)
        {
            Instantiate(skillSlot, ust_content);
        }
    }
    async Task OpenLoadoutMenu()
    {
        BuildUST(5);
        ust.transform.DOMove(ust_inner.position, 0.15f);
        est.transform.DOMove(est_inner.position, 0.15f);
        await Task.Delay(150);
    }
    async Task CloseLoadoutMenu()
    {
        ust.transform.DOMove(ust_outer.position, 0.15f);
        est.transform.DOMove(est_outer.position, 0.15f);
        await Task.Delay(150);
    }
}
