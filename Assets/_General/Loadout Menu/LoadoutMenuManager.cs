using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using static MapManager;
using static Database;
using System.Linq;
using Unity.VisualScripting;
using static SaveAndLoad;

/// <summary>
/// ST =>  Skills Tab
/// SP => Skill Pool
/// </summary>
public class LoadoutMenuManager : MonoBehaviour
{
    public static LoadoutMenuManager instance;

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
    public static event Action OnEntryMoved;

    [Header("Resources")]
    public SkillSlot skillSlotPrefab;
    public SkillEntry skillEntryPrefab;
    [Space]
    public MenuElement ST;
    public Transform skillTabContent;
    [Space]
    public MenuElement SP;
    public Transform skillPoolContent;

    public Dictionary<int, SkillSlot> slots = new();
    public List<SkillSlot> loadoutSlots = new();
    public Dictionary<int, SkillEntry> entries = new();
    #endregion

    private void Awake()
    {
        if(loadoutPanel.activeSelf) menuHistory.Push(loadoutPanel);
        instance = this;
    }
    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialized == true);

        loadoutPanel.SetActive(false);
        BuildSkillPool();
        ReconstructLoadout();

        loadout_open.AddEvent
        (
            EventTriggerType.PointerClick,
            async action => { // Function order is important.
                LoadAll();
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
                SaveAll();
                loadout_open.gameObject.SetActive(true);
                await CloseLoadoutMenu();
                loadoutPanel.SetActive(false);
                menuHistory.Pop(); 
            }
        );
    }
    private void OnEnable()
    {
        OnEntryMoved += CheckEquipped;
        FillSaveFile += () => generalSaveFile.skillData = skillData;
        FillSaveFile += DeconstructLoadout;
    }
    private void OnDisable()
    {
        OnEntryMoved -= CheckEquipped;
        FillSaveFile -= () => generalSaveFile.skillData = skillData;
        FillSaveFile -= DeconstructLoadout;
    }

    public void DeconstructLoadout()
    {
        skillData.equippedSkills.Clear();
        foreach (var item in equippedSkills)
        {
            if (item.Value == null || item.Value.data == null)
                continue;

            skillData.equippedSkills.Add(item.Key, item.Value.data.id);
        }

        skillData.loadoutPanel_entriesState.Clear();
        foreach (var item in entries)
        {
            skillData.loadoutPanel_entriesState.Add
                (
                    item.Key,
                    new SkillData.LoadoutEntryData
                    {
                        id = item.Value.id,
                        loadoutID = item.Value.loadoutID
                    }
                );
        }
    }
    public void ReconstructLoadout()
    {
        foreach (var item in skillData.equippedSkills)
        {
            equippedSkills[item.Key] = GetSkillById(item.Value);
        }

        foreach (var item in skillData.loadoutPanel_entriesState)
        {
            entries[item.Key].loadoutID = item.Value.loadoutID;
        }
    }

    async Task OpenLoadoutMenu()
    {
        ValidateEntries();

        SP.self.transform.DOMove(SP.innerPoint.position, 0.15f);
        ST.self.transform.DOMove(ST.innerPoint.position, 0.15f);
        await Task.Delay(150);
    }
    async Task CloseLoadoutMenu()
    {
        SP.self.transform.DOMove(SP.outerPoint.position, 0.15f);
        ST.self.transform.DOMove(ST.outerPoint.position, 0.15f);
        await Task.Delay(150);
    }

    void BuildSkillPool()
    {
        foreach(var id in AllUnlockedSkills)
        {
            var slot = Instantiate(skillSlotPrefab, skillPoolContent);
            slot.id = id;
            slots.Add(slot.id, slot);

            var entry = Instantiate(skillEntryPrefab, slot.transform);
            entry.id = id;
            entries.Add(entry.id, entry);
        }
    }
    void ValidateEntries()
    {
        foreach(var entry in entries.Values)
        {
            if(entry.IsEquipped)
            {
                entry.transform.SetParent(loadoutSlots[entry.loadoutID - 1].transform);
                entry.rectTransform.anchoredPosition = Vector2.zero;
                continue;
            }

            entry.transform.SetParent(slots[entry.id].transform);
            entry.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
    public void CheckEquipped()
    {
        foreach(var entry in entries.Values)
        {
            if (entry.IsEquipped) equippedSkills[entry.loadoutID] = entry.skill;
        }
    }

    public static void EntryMoved()
    {
        OnEntryMoved?.Invoke();
    }
}
