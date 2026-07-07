using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IBattleEntity
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

    public static event Action OnPlayerHealthChanged;

    public PlayerStatsSO stats;
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
    }
    private void Start()
    {
        SetInitialState();
    }
    private void FixedUpdate()
    {
        if(canGainActionPoints) actionPoints += stats.actionPointsSpeed * UnityEngine.Time.deltaTime;
        if (actionPoints >= 100 && !CombatManager.instance.actingEntities.Contains(this))
        {
            CombatManager.instance.actingEntities.Add(this);
        }
    }

    void SetDeathState() => isDead = true;
    private void OnEnable()
    {
        CombatManager.OnPlayerDeath += SetDeathState;
    }
    private void OnDisable()
    {
        CombatManager.OnPlayerDeath -= SetDeathState;
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
        stamina = Math.Clamp(stamina, 0, 15);
        StaminaChanged();
        isActing = true;
        CombatManager.Draw();
        yield return new WaitUntil(() => isActing == false);
        actionPoints = 0;
    }

    public int CalcDmg(int amount)
    {
        return 0;
    }

    public void TakeDamage(int amount)
    {
        stats.hp -= amount;
        PlayerHealthChanged();
        if(stats.hp <= 0) CombatManager.PlayerDeath();
    }
    #endregion Interface

    #endregion Methods

    #region Events
    public static void PlayerHealthChanged()
    {
        OnPlayerHealthChanged?.Invoke();
    }
    #endregion
    
}
