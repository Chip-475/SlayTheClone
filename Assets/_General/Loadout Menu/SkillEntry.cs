using UnityEngine;
using UnityEngine.UI;

public class SkillEntry : DragDropUI
{
    public Skill skill; 
    public Image image;

    public int id;

    new void Awake()
    {
       base.Awake();
        image = GetComponent<Image>();
    }
    private void Start()
    {
        image.sprite = skill.data.icon;
    }
}
