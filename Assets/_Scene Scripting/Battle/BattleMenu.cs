using UnityEngine;
using System.Collections.Generic;
using static Database;

public class BattleMenu : MonoBehaviour
{
    public Stack<GameObject> menuHistory = new();

    public CanvasGroup menuGroup;
    public GameObject baseMenu;

    public GameObject skillButtonPrefab;
    public GameObject skillButtonPrefab_empty;
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
    [ContextMenu("Build Skills")]
    public void BuildSkillButtons()
    {
        for (int i = 0; i < 6; i++)
        {
            var button = Instantiate
            (
                skillButtonPrefab,
                skillButtonsPositions[i].position,
                Quaternion.identity,
                baseMenu.transform
            );

            SkillButton sb = button.GetComponent<SkillButton>();

            if (!equippedSkills.ContainsKey(i + 1)) continue;
            sb.skill = equippedSkills[i + 1];
        }
    }
    #endregion
}
