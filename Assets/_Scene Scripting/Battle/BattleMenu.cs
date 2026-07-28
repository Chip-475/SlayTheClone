using UnityEngine;
using System.Collections.Generic;

public class BattleMenu : MonoBehaviour
{
    public Stack<GameObject> menuHistory = new();

    public GameObject baseMenu;
    public GameObject attackMenu;
    public GameObject defendMenu;

    private void Start()
    {
        menuHistory.Push(baseMenu);
    }

    #region Methods
    public void OnAttackClick()
    {
        attackMenu.SetActive(true);
        menuHistory.Push(attackMenu);
    }
    public void OnDefendClick()
    {
        defendMenu.SetActive(true);
        menuHistory.Push(defendMenu);
    }
    public void OnPassClick()
    {
        if (!CombatManager.instance.player.isActing) return;

        CombatManager.instance.player.isActing = false;
    }
    public void OnBackClick()
    {
        menuHistory.Pop().SetActive(false);
    }
    #endregion
}
