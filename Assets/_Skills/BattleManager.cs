using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class BattleManager : MonoBehaviour
{
    [Serializable]
    public class BattleAction
    {
        public int actionId;

        public List<Enemy> targetedEnemies;
        public SkillCard selectedSkill;
    }
    public List<BattleAction> actions = new();
    public int numberOfActions;

    public List<Enemy> enemyList = new();
    public SkillCard currentSelected;
    public List<Enemy> currentTargets = new();

    public static BattleManager instance;
    private void Start()
    {
        instance = this;
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemyList = new List<Enemy>(enemies);
    }

    public void BuildAction()
    {
        var action = new BattleAction();
        action.actionId = numberOfActions;
        action.selectedSkill = currentSelected;
        action.targetedEnemies = new List<Enemy>(currentTargets);
        actions.Add(action);

        currentSelected = null;
        currentTargets.Clear();
    }
    public IEnumerator StartCombatCR()
    {
        for(int i = 0; i < numberOfActions; i++)
        {
            yield return StartCoroutine(actions[i].selectedSkill.skill.OnUse(actions[i].targetedEnemies));
            HandManager.instance.cardsInHand.Remove(actions[i].selectedSkill);
            Destroy(actions[i].selectedSkill.gameObject);
            HandManager.instance.SetCards(0.15f);
        }
        actions.Clear();
        GameManager.State = GameManager.GameState.None;
    }
    public void _StartCombat()
    {
        StartCoroutine(StartCombatCR());
    }
}
