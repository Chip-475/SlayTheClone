using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MapManager;
using static Database;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// EST => Equipped Skills Tab
/// UST => Unlocked Skills Tab
/// </summary>
public class LoadoutMenuManager : MonoBehaviour
{
    public GameObject loadoutPanel;
    public EventTrigger loadout_open;
    public EventTrigger loadout_close;
    [Serializable] public struct MenuElement
    {
        public GameObject self;
        public Transform innerPoint;
        public Transform outerPoint;
    }

    #region Loadout Panel
    [Header("Resources")]
    public SkillSlot skillSlotPrefab;
    public SkillEntry skillEntryPrefab;
    [Space]
    public MenuElement EST;
    public Transform estContent;
    [Space]
    public MenuElement UST;
    public Transform ustContent;

    [Header("Data")]
    public Dictionary<int, SkillSlot> slots = new();
    public Dictionary<int, SkillEntry> entries = new();
    #endregion

    private void Awake()
    {
        if(loadoutPanel.activeSelf) menuHistory.Push(loadoutPanel);
    }
    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialized == true);

        loadoutPanel.SetActive(false);
        BuildSlots();

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

    // instantiate 1 entry per unlocked skill
    // have each slot have a unique id
    // have each entry have a unique id
    // sync entry id to slot id
    void BuildSlots()
    {
        if (slots.Keys.Count != 0) return;

        int id = 1;
        for(; id <= 6; id++)
        {
            print(id);
            var slot = Instantiate(skillSlotPrefab, estContent);
            slot.id = id;
            slot.GetComponent<Image>().color = new Color(0, 0, 0, 0);

            slots.Add(id, slot);
        }
        foreach(var skill in unlockedSkills)
        {
            print(id);
            var slot = Instantiate(skillSlotPrefab, ustContent);
            slot.id = id;

            slots.Add(id, slot);
            id++;
        }
    }

    async Task OpenLoadoutMenu()
    {
        UST.self.transform.DOMove(UST.innerPoint.position, 0.15f);
        EST.self.transform.DOMove(EST.innerPoint.position, 0.15f);
        await Task.Delay(150);
    }
    async Task CloseLoadoutMenu()
    {
        UST.self.transform.DOMove(UST.outerPoint.position, 0.15f);
        EST.self.transform.DOMove(EST.outerPoint.position, 0.15f);
        await Task.Delay(150);
    }
}
