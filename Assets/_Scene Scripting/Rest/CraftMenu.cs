using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftMenu : MonoBehaviour
{
    #region Declarations
    public List<RecipeSO> recipes;
    public GameObject menuPanel;
    #endregion

    #region Unity Methods
    private void Start()
    {
        foreach (var recipe in RecipeDatabase.GetUnlockedRecipes()) recipes.Add(recipe);
    }
    #endregion

    #region Methods
    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
    #endregion
}
