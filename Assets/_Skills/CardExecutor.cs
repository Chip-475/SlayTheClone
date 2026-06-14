using UnityEngine;

public class CardExecutor : MonoBehaviour
{
    public static SkillCard cardInUse;
    public static IBattleEntity target;

    public void ExecuteCard()
    {
        if (target == null || cardInUse == null) return;
    }
}
