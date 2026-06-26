using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IBattleEntity
{
    public class BattleAction
    {
        public static Player player;
        public SkillCard card;
        public Enemy target;

        /// <summary>
        /// Executes a skill on a target enemy.
        /// </summary>
        public void Execute()
        {
            var runner = CoRoutineRunner.instance;
            runner.StartCoroutine(ExecuteCR(runner));
        }
        IEnumerator ExecuteCR(MonoBehaviour runner)
        {
            yield return runner.StartCoroutine(card.skill.Effect(target));
            CombatManager.instance.hand.cardsInHand.Remove(card);
            Destroy(card.gameObject);
            //HandManager.instance.SetCards(0.15f);
            player.stamina -= card.skill.cost;
            player.StaminaChanged();

            selecting = false;

            print("BattleAction executed successfully!");

            Player.cardInUse = null;
            Player.target = null;
        }

        /// <summary>
        /// Builds a BattleAction using Card and targeting Enemy.
        /// </summary>
        public static BattleAction Build(SkillCard Card, Enemy Enemy)
        {
            BattleAction actionToReturn = new()
            {
                card = Card,
                target = Enemy
            };

            return actionToReturn;
        }
    }

    #region Declarations
    public static event Action OnPlayerHealthChanged;

    public PlayerStatsSO stats;
    public int id;
    public bool canGainActionPoints;
    public float actionPoints;

    public int stamina;
    [SerializeField] private TMP_Text _staminaText;
    public static bool selecting;
    public bool isActing;
    public static SkillCard cardInUse = null;
    public static Enemy target = null;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        CombatManager.instance.player = this;
        BattleAction.player = this;
    }
    private void Start()
    {
        SetInitialState();
    }
    private void FixedUpdate()
    {
        if(canGainActionPoints) actionPoints += stats.actionPointsSpeed * Time.deltaTime;
        if (actionPoints >= 100 && !CombatManager.instance.actingEntities.Contains(this))
        {
            CombatManager.instance.actingEntities.Add(this);
        }

        // TO REFINE LATER
        if(cardInUse != null && target != null)
        {
            var action = BattleAction.Build(cardInUse, target);
            action.Execute();
        }
    }
    #endregion

    #region Methods
    void SetInitialState()
    {
        id = 0;
        actionPoints = 0;
        canGainActionPoints = true;
        
        stamina = 5;
        isActing = false;
    }
    public void StaminaChanged()
    {
        _staminaText.text = $"{stamina}";
    }
    public void EndTurn()
    {
        if (!isActing) return;

        isActing = false;
        print("Player turn ended.");
    }
    #endregion

    #region Events
    public static void PlayerHealthChanged()
    {
        OnPlayerHealthChanged?.Invoke();
    }
    #endregion

    #region Interface
    public int GetId()
    {
        return id;
    }
    public void StopActionBar()
    {
        canGainActionPoints = false;
    }
    public void StartActionBar()
    {
        canGainActionPoints = true;
    }

    public IEnumerator Action()
    {
        stamina += 3;
        stamina = Math.Clamp(stamina ,0, 15);
        StaminaChanged();
        isActing = true;
        //Hand.instance.DrawCards();
        yield return new WaitUntil(() => isActing == false);
        actionPoints = 0;
    }

    public void CalcDmg(int amount, List<DamageTypeSO> types)
    {
        return;
    }
    public void TakeDamage(int amount)
    {
        stats.hp -= amount;
        PlayerHealthChanged();
    }
    #endregion
}
