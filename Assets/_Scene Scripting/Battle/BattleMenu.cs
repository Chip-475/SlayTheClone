using UnityEngine;
using System.Collections.Generic;
using static Database;

public class BattleMenu : MonoBehaviour
{
    public Stack<GameObject> menuHistory = new();

    public CanvasGroup menuGroup;
    public GameObject baseMenu;

    public GameObject skillButtonPrefab;
    public List<RectTransform> skillButtonsPositions = new();

    private void Start()
    {
        menuHistory.Push(baseMenu);

        menuGroup.interactable = false;
        BuildSkillButtons();
    }

    #region Methods
    public void OnPassClick()
    {
        if (!CombatManager.instance.player.isActing) return;

        CombatManager.instance.player.isActing = false;
    }
    public void BuildSkillButtons()
    {
        int i = 0;
        foreach (var point in skillButtonsPositions)
        {
            if (i >= equippedSkills.Count) continue;

            var button = Instantiate
            (
                skillButtonPrefab,
                point.position,
                Quaternion.identity,
                baseMenu.transform
            );

            SkillButton sb = button.GetComponent<SkillButton>();
            sb.skill = GetSkill(equippedSkills[i]);

            i++;
        }
    }
    #endregion
}
