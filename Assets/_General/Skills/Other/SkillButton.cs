using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    public Skill skill;

    TMP_Text text;
    Button button;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
    }
    private void Start()
    {
        if(skill == null) { button.interactable = false; return; }

        text.text = skill.name;
        button.onClick.AddListener(skill.Select);
    }
}
