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

    public PlayerStats Stats => PlayerManager.instance.stats;
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
        PlayerManager.player = this;
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
        CombatManager.instance.battleMenu.menuGroup.interactable = true;
        isActing = true;
        yield return new WaitUntil(() => isActing == false);
        CombatManager.instance.battleMenu.menuGroup.interactable = false;
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

        if(Stats.hp <= 0 && !isDead)
        {
            CombatManager.instance.StartCoroutine(DeathSequence());
        }
    }
    public IEnumerator DeathSequence()
    {
        isDead = true;
        ChangeAnimation("Death");
        CombatManager.instance.SlowTime();
        CombatManager.instance.OpenDeathPanel();

        yield break;
    }

    public void ChangeAnimation(string anim)
    {
        animator.CrossFade(anim, 0, 0);
    }
    #endregion Methods

    #region Events
    public static void PlayerHealthChanged()
    {
        OnPlayerHealthChanged?.Invoke();
    }
    #endregion
}
