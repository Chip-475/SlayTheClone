using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimController))]
public class Player : MonoBehaviour, IBattleEntity
{
    #region Declarations
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
    public static event Action OnPlayerStaminaChanged;

    public PlayerStats Stats => PlayerManager.stats;
    public PlayerAnimController animController;
    public Animator animator;

    public int id;
    public int ID { get { return id; } set { id = value; } }

    public bool canGainActionPoints;
    public float actionPoints;

    int _stamina;
    [SerializeField] private TMP_Text _staminaText;
    public static bool isDead = false;
    public static bool selecting;
    public bool isActing;
    #endregion
    #region Properties
    public int Stamina 
    { 
        get { return _stamina; } 
        set {
            int clamped = Math.Clamp(value, 0, 15);

            if (_stamina == clamped)
                return;

            _stamina = clamped;
            OnPlayerStaminaChanged?.Invoke();
        } 
    }
    public int Health
    {
        get { return Stats.hp; }
        set
        {
            int clamped = Math.Clamp(value, 0, Stats.maxHp);

            if (Stats.hp == clamped)
                return;

            Stats.hp = clamped;
            OnPlayerHealthChanged?.Invoke();
        }
    }

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

        OnPlayerStaminaChanged?.Invoke();
    }
    private void FixedUpdate()
    {
        if(canGainActionPoints) actionPoints += Stats.actionPointsSpeed * UnityEngine.Time.deltaTime;
        if (actionPoints >= 100 && !CombatManager.instance.actingEntities.Contains(this))
        {
            CombatManager.instance.actingEntities.Add(this);
            CombatManager.instance.StartCoroutine(CombatManager.PerformActions());
        }
    }
    private void OnEnable()
    {
        OnPlayerStaminaChanged += () => _staminaText.text = Stamina.ToString();
    }
    private void OnDisable()
    {
        OnPlayerStaminaChanged -= () => _staminaText.text = Stamina.ToString();
    }
    #endregion

    #region Methods
    void SetInitialState()
    {
        id = 0;
        actionPoints = 0;
        canGainActionPoints = true;
        
        _stamina = 5;
        isActing = false;
    }

    public void ActionBarMovement(bool active)
    {
        if (active) { canGainActionPoints = true; }
        else { canGainActionPoints = false; }
    }

    public IEnumerator Action()
    {
        Stamina += 3;
        CombatManager.instance.battleMenu.menuGroup.interactable = true;
        isActing = true;
        yield return new WaitUntil(() => isActing == false);
        CombatManager.instance.battleMenu.menuGroup.interactable = false;
        actionPoints = 0;
    }

    public int ApplyResistances(int amount) { return 0; }
    public void TakeDamage(int amount)
    {
        Health -= amount;

        if(Health <= 0 && !isDead)
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
