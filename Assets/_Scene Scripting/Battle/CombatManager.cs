using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor.UIElements;

public class CombatManager : MonoBehaviour
{
    #region Declarations
    public static CombatManager instance;
    public MainDatabase Database => MainDatabase.instance;

    [Header("References")]
    public Battle battle;
    public SkillExecutor skillExecutor;
    public BattleMenu battleMenu;
    [Space]
    public Player player;
    public GameObject deathScreen;
    [Space]
    public bool entitiesAreActing = false;
    public List<Enemy> enemiesOnField = new();
    public List<IBattleEntity> actingEntities = new();
    public BattleResults results = new();

    public Skill selectedSkill;
    public Enemy selectedEnemy;

    public GameObject fadePanel;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;
        
        battle = GetComponent<Battle>();
        skillExecutor = GetComponent<SkillExecutor>();
        battleMenu = GetComponent<BattleMenu>();
    }
    private void Start()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f);

        deathScreen.SetActive(false);

        battle.SpawnBackground();
        battle.SpawnEnemies();

        StartCoroutine(BattleWinCR());
    }
    #endregion

    #region Methods
    public static void PerformActions()
    {
        instance.StartCoroutine(instance.PerformActionsCR(instance.actingEntities));
    }
    public IEnumerator PerformActionsCR(List<IBattleEntity> entities)
    {
        entitiesAreActing = true;

        StopActionBars();
        entities.Sort((a, b) => a.GetId().CompareTo(b.GetId()));
        foreach (var entity in entities)
        {
            yield return StartCoroutine(entity.Action());
        }
        entities.Clear();
        StartActionBars();

        entitiesAreActing = false;
    }

    public void StopActionBars()
    {
        foreach (var entity in enemiesOnField)
        {
            entity.StopActionBar();
        }
        player.StopActionBar();
    }
    public void StartActionBars()
    {
        foreach (var entity in enemiesOnField)
        {
            entity.StartActionBar();
        }
        player.StartActionBar();
    }

    IEnumerator BattleWinCR()
    {
        yield return new WaitUntil(() => enemiesOnField.Count == 0 && !Player.isDead);

        player.ChangeAnimation("Win"); yield return new WaitForSeconds(3);

        // show results screen
        results.Validate();

        fadePanel.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadSceneAsync("Map", LoadSceneMode.Single);
    }

    public void SlowTime()
    {
        DOVirtual.Float
        (
            1,
            0,
            2f,
            value =>
            {
                Time.timeScale = value; ;
            }
        );
    }
    public void OpenDeathPanel()
    {
        deathScreen.SetActive(true);
        var panelGroup = deathScreen.GetComponent<CanvasGroup>();
        
        panelGroup.alpha = 0f;
        panelGroup.interactable = false;

        panelGroup.DOFade(1f, 0.5f)
            .SetEase(Ease.InQuad)
            .OnComplete(() => panelGroup.interactable = true);
    }
    #endregion
}
public class BattleResults
{
    public Dictionary<ItemSO, int> drops = new();

    public void Validate()
    {
        foreach (var item in drops)
        {
            ItemDatabase.GetItem(item.Key.id).amount = item.Value;
        }
    }
}
