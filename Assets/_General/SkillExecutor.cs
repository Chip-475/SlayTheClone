using UnityEngine;
using System.Collections;

#pragma warning disable
public class SkillExecutor : MonoBehaviour
{
    #region Declarations
    MainDatabase Database => MainDatabase.instance;

    public static Player player => CombatManager.instance.player;
    public Skill skill => CombatManager.instance.selectedSkill;
    public Enemy enemy => CombatManager.instance.selectedEnemy;
    #endregion

    #region Methods
    public void Execute()
    {
        var runner = CombatManager.instance;
        runner.StartCoroutine(ExecuteCR(runner));
    }
    IEnumerator ExecuteCR(MonoBehaviour runner)
    {
        skill.Effect(enemy);
        yield break;
    }
    #endregion
}
