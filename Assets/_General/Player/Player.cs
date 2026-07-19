using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimController))]
public class Player : MonoBehaviour, IBattleEntity
{
    #region Declarations
    MainDatabase Database => MainDatabase.instance;

    public enum State
    {
        Idle,
        LightAttacking,
        HeavyAttacking,
        Casting,
        Dead
    }
    public State state;

    public static event Action OnPlayerHealthChanged;

    public MainDatabase.PlayerStats Stats => Database.playerStats;
    public PlayerAnimController animController;
    public Animator animator;

    public int id;
    public bool canGainActionPoints;
    public float actionPoints;

    public int stamina;
    [SerializeField] private TMP_Text _staminaText;
    public static bool isDead = false;
    public static bool selecting;
    public bool isActing;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        CombatManager.instance.player = this;

        animController = GetComponent<PlayerAnimController>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        SetInitialState();

        state = State.Idle;
    }
    private void FixedUpdate()
    {
        if(canGainActionPoints) actionPoints += Stats.actionPointsSpeed * UnityEngine.Time.deltaTime;
        if (actionPoints >= 100 && !CombatManager.instance.actingEntities.Contains(this))
        {
            CombatManager.instance.actingEntities.Add(this);
            CombatManager.PerformActions();
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
    }

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
        stamina = Math.Clamp(stamina, 0, 15);
        StaminaChanged();
        isActing = true;
        CombatManager.Draw();
        yield return new WaitUntil(() => isActing == false);
        actionPoints = 0;
    }
    public int ApplyResistances(int amount)
    {
        return 0;
    }

    public void TakeDamage(int amount)
    {
        Stats.hp -= amount;
        PlayerHealthChanged();

        if(Stats.hp <= 0) CombatManager.instance.StartCoroutine(DeathSequence());
    }
    public IEnumerator DeathSequence()
    {
        isDead = true;
        CombatManager.instance.SlowTime();
        CombatManager.instance.OpenDeathPanel();

        yield break;
    }
    #endregion Methods

    #region Events
    public static void PlayerHealthChanged()
    {
        OnPlayerHealthChanged?.Invoke();
    }
    #endregion
}
