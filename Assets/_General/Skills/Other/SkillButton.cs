using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static PlayerManager;

public class SkillButton : MonoBehaviour
{
    public Skill skill;

    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text costText;
    Button button;
    EventTrigger trigger;

    private void Awake()
    {
        button = GetComponent<Button>();
        trigger = GetComponent<EventTrigger>();
    }
    private void Start()
    {
        if(skill == null) { button.interactable = false; return; }

        text.text = skill.data.skillName;
        costText.text = skill.data.staminaCost.ToString();
        button.onClick.AddListener(skill.Select);

        trigger.AddEvent(EventTriggerType.PointerEnter, action => skill.OnPointerEnter());
        trigger.AddEvent(EventTriggerType.PointerExit, action => skill.OnPointerExit());
    }
    private void OnEnable() { Player.OnHealthChanged += UpdateInteractable; }
    private void OnDisable() { Player.OnHealthChanged -= UpdateInteractable; }

    public void UpdateInteractable()
    {
        if (button == null || player == null || skill == null || skill.data == null)
            return;

        button.interactable = player.Stamina >= skill.data.staminaCost;
    }
}
