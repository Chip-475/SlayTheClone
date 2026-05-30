using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class BattleManager : MonoBehaviour
{
    [Serializable]
    public class PlayerBattleAction
    {
        public int actionId;

        public List<Enemy> targetedEnemies;
        public CardSkill selectedSkill;
    }
    public List<PlayerBattleAction> actions = new();
    public int numberOfActions;

    public List<Enemy> enemyList = new();
    public List<Transform> spawnPoints = new();

    public CardSkill currentSelected;
    public List<Enemy> currentTargets = new();

    public static BattleManager instance;
    private void Start()
    {
        instance = this;

        _SpawnEnemies();
    }

    public void BuildPlayerAction()
    {
        var action = new PlayerBattleAction();
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

    public IEnumerator SpawnEnemiesCR()
    {
        for(int i = 0; i < spawnPoints.Count; i++)
        {
            var index = UnityEngine.Random.Range(0, Database.instance.enemyPrefabs.Count);
            var enemy = Database.instance.enemyPrefabs[index];
            enemy.group.sortingOrder = -i;
            Instantiate(enemy, spawnPoints[i].position, Quaternion.identity);
            enemyList.Add(enemy);
        }

        yield return null;
    }
    public void _SpawnEnemies()
    {
        StartCoroutine(SpawnEnemiesCR());
    }
}
