using UnityEngine;
using System.Collections.Generic;

public class BattleMenu : MonoBehaviour
{
    public Stack<GameObject> menuHistory = new();

    public CanvasGroup menuGroup;
    public GameObject baseMenu;

    public List<RectTransform> skillButtonsPositions = new();

    private void Start()
    {
        menuHistory.Push(baseMenu);

        menuGroup.interactable = false;
    }

    #region Methods
    public void OnPassClick()
    {
        if (!CombatManager.instance.player.isActing) return;

        CombatManager.instance.player.isActing = false;
    }
    #endregion
}
