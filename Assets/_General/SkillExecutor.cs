using UnityEngine;
using System.Collections;

#pragma warning disable
public class SkillExecutor : MonoBehaviour
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

    public static Player player => CombatManager.instance.player;
    public SkillCard card => CombatManager.instance.selectedCard;
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
        yield return runner.StartCoroutine(card.skill.Effect(enemy));
        CombatManager.instance.hand.cardsInHand.Remove(card);
        CombatManager.instance.deck.deckQueue.Enqueue(card);
        card.gameObject.SetActive(false);
        player.stamina -= card.skill.cost;
        player.StaminaChanged();

        Player.selecting = false;

        print("BattleAction executed successfully!");

        CombatManager.instance.selectedCard = null;
        CombatManager.instance.selectedEnemy = null;
    }
    #endregion
}
