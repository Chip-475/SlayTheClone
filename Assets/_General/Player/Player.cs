using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

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

    public static event Action OnHealthChanged;
    public static event Action OnStaminaChanged;

    public PlayerStats Stats => PlayerManager.P_Stats;
    public Animator animator;

    int id;

    public bool canGainActionPoints;
    public float actionPoints;

    int stamina;
    [SerializeField] private TMP_Text staminaText;
    public static bool isDead = false;
    public static bool selecting;
    public bool isActing;
    #endregion
    #region Properties
    public int ID { get { return id; } set { id = value; } }
    
    public int Health
    {
        get { return Stats.hp; }
        set
        {
            int clamped = Math.Clamp(value, 0, Stats.maxHp);

            if (Stats.hp == clamped)
                return;

            Stats.hp = clamped;
            OnStaminaChanged?.Invoke();
        }
    }
    public int Stamina
    {
        get { return stamina; }
        set
        {
            int clamped = Math.Clamp(value, 0, 8);

            if (stamina == clamped)
                return;

            stamina = clamped;
            OnHealthChanged?.Invoke();
        }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        CombatManager.instance.player = this;

        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        state = State.Idle;
        PlayerManager.player = this;

        Init();
        OnHealthChanged?.Invoke();
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
        OnHealthChanged += () => staminaText.text = Stamina.ToString();
    }
    private void OnDisable()
    {
        OnHealthChanged -= () => staminaText.text = Stamina.ToString();
    }
    #endregion

    #region Methods
    void Init()
    {
        ID = 0;
        Stamina = 3;
        Health = PlayerManager.P_Stats.maxHp;

        actionPoints = 0;
        canGainActionPoints = true;

        isActing = false;
    }

    public void ActionBarMovement(bool active)
    {
        if (active) { canGainActionPoints = true; }
        else { canGainActionPoints = false; }
    }
    public IEnumerator Action()
    {
        CombatManager.instance.battleMenu.menuGroup.interactable = true;
        Stamina += 2;
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
        Database.newFile = true;
        ChangeAnimation("Death");
        CombatManager.instance.OpenDeathPanel();

        yield break;
    }

    public void ChangeAnimation(string anim)
    {
        animator.CrossFade(anim, 0, 0);
    }
    #endregion Methods
}
