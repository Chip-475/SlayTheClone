using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public class BattleAction
    {
        public List<Enemy> targetedEnemies;
        public SkillCard selectedSkill;
    }
    public List<BattleAction> actions;

    public List<Enemy> enemyList = new();

    public static BattleManager instance;
    private void Start()
    {
        instance = this;
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach(var temp in enemies)
        {
            enemyList.Add(temp);
        }
    }

    public void UseSkill()
    {

    }
}
