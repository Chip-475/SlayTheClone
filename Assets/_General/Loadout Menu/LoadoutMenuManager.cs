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

        loadout_open.AddEvent
        (
            EventTriggerType.PointerClick,
            async action => { // Function order is important.
                LoadLoadoutMenu();
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
                SaveLoadoutMenu();
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
    }
    private void OnDisable()
    {
        OnEntryMoved -= CheckEquipped;
    }

    void SaveLoadoutMenu()
    {
        skillData.loadoutPanel_entriesState.Clear();

        foreach(var entry in entries)
        {
            skillData.loadoutPanel_entriesState.Add
                (
                    entry.Key,
                    new SkillData.LoadoutEntryData
                    {
                        id = entry.Value.id,
                        loadoutID = entry.Value.loadoutID
                    }
                );
        }

        SaveAndLoad.Save(skillData, Database.instance.skillDataPath);
    }
    void LoadLoadoutMenu()
    {
        skillData = SaveAndLoad.Load<SkillData>(Database.instance.skillDataPath);

        foreach(var entry in skillData.loadoutPanel_entriesState)
        {
            entries[entry.Key].id = entry.Value.id;
            entries[entry.Key].loadoutID = entry.Value.loadoutID;
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
