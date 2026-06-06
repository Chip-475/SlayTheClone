using System;
using UnityEngine;

public class EventSystemDemo : MonoBehaviour
{
    [SerializeField] private bool runDemoOnStart = true;

    private DemoBattleEvents battleEvents;
    private DemoPlayer player;
    private DemoEnemy enemy;
    private DemoCombatLog combatLog;
    private DemoSoundSystem soundSystem;
    private DemoAchievementTracker achievementTracker;

    private void Awake()
    {
        battleEvents = new DemoBattleEvents();

        player = new DemoPlayer("Knight", 30, battleEvents);
        enemy = new DemoEnemy("Training Dummy", 18, battleEvents);

        combatLog = new DemoCombatLog(battleEvents);
        soundSystem = new DemoSoundSystem(battleEvents);
        achievementTracker = new DemoAchievementTracker(battleEvents);
    }

    private void Start()
    {
        if (runDemoOnStart)
        {
            RunDemoBattle();
        }
    }

    private void OnDestroy()
    {
        combatLog.Unsubscribe();
        soundSystem.Unsubscribe();
        achievementTracker.Unsubscribe();
    }

    [ContextMenu("Run Event System Demo")]
    private void RunDemoBattle()
    {
        Debug.Log("=== EVENT SYSTEM DEMO START ===");

        player.PlayCard("Light Strike", enemy, 7);
        enemy.Attack(player, 5);
        player.PlayCard("Swipe", enemy, 12);

        Debug.Log("=== EVENT SYSTEM DEMO END ===");
    }
}

class DemoBattleEvents
{
    public event Action<string> CardPlayed;
    public event Action<string, int, int> HealthChanged;
    public event Action<string, string, int> DamageDealt;
    public event Action<string> CharacterDied;

    public void RaiseCardPlayed(string cardName)
    {
        CardPlayed?.Invoke(cardName);
    }

    public void RaiseHealthChanged(string characterName, int currentHealth, int maxHealth)
    {
        HealthChanged?.Invoke(characterName, currentHealth, maxHealth);
    }

    public void RaiseDamageDealt(string attackerName, string targetName, int damage)
    {
        DamageDealt?.Invoke(attackerName, targetName, damage);
    }

    public void RaiseCharacterDied(string characterName)
    {
        CharacterDied?.Invoke(characterName);
    }
}

class DemoCharacter
{
    private readonly DemoBattleEvents battleEvents;

    public string Name { get; }
    public int MaxHealth { get; }
    public int CurrentHealth { get; private set; }

    protected DemoCharacter(string name, int maxHealth, DemoBattleEvents battleEvents)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        this.battleEvents = battleEvents;
    }

    public void TakeDamage(string attackerName, int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        battleEvents.RaiseDamageDealt(attackerName, Name, damage);
        battleEvents.RaiseHealthChanged(Name, CurrentHealth, MaxHealth);

        if (CurrentHealth == 0)
        {
            battleEvents.RaiseCharacterDied(Name);
        }
    }
}

class DemoPlayer : DemoCharacter
{
    private readonly DemoBattleEvents battleEvents;

    public DemoPlayer(string name, int maxHealth, DemoBattleEvents battleEvents)
        : base(name, maxHealth, battleEvents)
    {
        this.battleEvents = battleEvents;
    }

    public void PlayCard(string cardName, DemoEnemy target, int damage)
    {
        battleEvents.RaiseCardPlayed(cardName);
        target.TakeDamage(Name, damage);
    }
}

class DemoEnemy : DemoCharacter
{
    public DemoEnemy(string name, int maxHealth, DemoBattleEvents battleEvents)
        : base(name, maxHealth, battleEvents)
    {
    }

    public void Attack(DemoPlayer target, int damage)
    {
        target.TakeDamage(Name, damage);
    }
}

class DemoCombatLog
{
    private readonly DemoBattleEvents battleEvents;

    public DemoCombatLog(DemoBattleEvents battleEvents)
    {
        this.battleEvents = battleEvents;

        battleEvents.CardPlayed += OnCardPlayed;
        battleEvents.HealthChanged += OnHealthChanged;
        battleEvents.DamageDealt += OnDamageDealt;
        battleEvents.CharacterDied += OnCharacterDied;
    }

    public void Unsubscribe()
    {
        battleEvents.CardPlayed -= OnCardPlayed;
        battleEvents.HealthChanged -= OnHealthChanged;
        battleEvents.DamageDealt -= OnDamageDealt;
        battleEvents.CharacterDied -= OnCharacterDied;
    }

    private void OnCardPlayed(string cardName)
    {
        Debug.Log($"Combat Log: played card '{cardName}'.");
    }

    private void OnHealthChanged(string characterName, int currentHealth, int maxHealth)
    {
        Debug.Log($"Combat Log: {characterName} health is {currentHealth}/{maxHealth}.");
    }

    private void OnDamageDealt(string attackerName, string targetName, int damage)
    {
        Debug.Log($"Combat Log: {attackerName} dealt {damage} damage to {targetName}.");
    }

    private void OnCharacterDied(string characterName)
    {
        Debug.Log($"Combat Log: {characterName} died.");
    }
}

class DemoSoundSystem
{
    private readonly DemoBattleEvents battleEvents;

    public DemoSoundSystem(DemoBattleEvents battleEvents)
    {
        this.battleEvents = battleEvents;

        battleEvents.CardPlayed += OnCardPlayed;
        battleEvents.DamageDealt += OnDamageDealt;
        battleEvents.CharacterDied += OnCharacterDied;
    }

    public void Unsubscribe()
    {
        battleEvents.CardPlayed -= OnCardPlayed;
        battleEvents.DamageDealt -= OnDamageDealt;
        battleEvents.CharacterDied -= OnCharacterDied;
    }

    private void OnCardPlayed(string cardName)
    {
        Debug.Log($"Sound System: play card sound for '{cardName}'.");
    }

    private void OnDamageDealt(string attackerName, string targetName, int damage)
    {
        Debug.Log("Sound System: play hit sound.");
    }

    private void OnCharacterDied(string characterName)
    {
        Debug.Log("Sound System: play victory/death sound.");
    }
}

class DemoAchievementTracker
{
    private readonly DemoBattleEvents battleEvents;

    public DemoAchievementTracker(DemoBattleEvents battleEvents)
    {
        this.battleEvents = battleEvents;

        battleEvents.CardPlayed += OnCardPlayed;
        battleEvents.CharacterDied += OnCharacterDied;
    }

    public void Unsubscribe()
    {
        battleEvents.CardPlayed -= OnCardPlayed;
        battleEvents.CharacterDied -= OnCharacterDied;
    }

    private void OnCardPlayed(string cardName)
    {
        Debug.Log($"Achievement Tracker: count '{cardName}' as one card played.");
    }

    private void OnCharacterDied(string characterName)
    {
        Debug.Log($"Achievement Tracker: record defeat of {characterName}.");
    }
}
