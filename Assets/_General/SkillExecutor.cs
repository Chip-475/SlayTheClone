using UnityEngine;
using System.Collections;

#pragma warning disable
public class SkillExecutor : MonoBehaviour
{
    #region Declarations
    CombatManager Manager => CombatManager.instance;
    #endregion

    #region Methods
    public IEnumerator SkillExecutionCR(MonoBehaviour runner)
    {
        yield return new WaitUntil(() => Manager.selectedSkill != null && Manager.selectedEnemy != null);

        if(Manager.selectedSkill.data.skillName == "Minor Heal" || Manager.selectedSkill.data.skillName == "Major Heal")
        {
            Manager.selectedSkill.Effect(PlayerManager.player);
        }
        else Manager.selectedSkill.Effect(Manager.selectedEnemy);

        Manager.selectedSkill = null;
        Manager.selectedEnemy = null;

        StartCoroutine(SkillExecutionCR(Manager));
    }
    #endregion
}
