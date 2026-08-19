using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class CombatManager : MonoBehaviour
{
    #region Declarations
    public static CombatManager instance;
    public Database Database => Database.instance;

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
        StartCoroutine(skillExecutor.SkillExecutionCR(this));
    }
    #endregion

    #region Methods
    public async void To_MM()
    {
        await fadePanel.GetComponent<CanvasGroup>().DOFade(1, 0.3f).AsyncWaitForCompletion();
        await SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public static IEnumerator PerformActions()
    {
        instance.entitiesAreActing = true;

        instance.AllActionBarsMovement(false);
        instance.actingEntities.Sort((a, b) => a.ID.CompareTo(b.ID));
        foreach (var entity in instance.actingEntities.ToList())
        {
            yield return instance.StartCoroutine(entity.Action());
        }
        instance.actingEntities.Clear();
        instance.AllActionBarsMovement(true);

        instance.entitiesAreActing = false;
    }

    public void AllActionBarsMovement(bool active)
    {
        if (active)
        {
            foreach (var entity in enemiesOnField)
            {
                entity.ActionBarMovement(active);
            }
            player.ActionBarMovement(active);
        }
        else
        {
            foreach (var entity in enemiesOnField)
            {
                entity.ActionBarMovement(active);
            }
            player.ActionBarMovement(active);
        }
        
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
