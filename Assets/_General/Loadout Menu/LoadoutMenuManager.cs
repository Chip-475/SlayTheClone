using UnityEngine;
using System.Collections.Generic;

public class LoadoutMenuManager : MonoBehaviour
{
    // Skills
    public GameObject skillsTab;
    public Transform skillsTabContent;
    public DraggableItemUI skillsTabDraggable;
    public LoadoutSlot skillsTabSlot;

    void Start()
    {
        skillsTabContent = Database.FindDeepChild(skillsTab.transform, "Content");

        BuildSkillsTab();
    }
    int i = 0;
    public void BuildSkillsTab()
    {
        Instantiate(skillsTabDraggable, skillsTabContent);
        Instantiate(skillsTabSlot, skillsTab.transform);

        i++;
        if(i < 10) BuildSkillsTab();
    }
}
