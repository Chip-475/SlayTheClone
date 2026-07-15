using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftMenu : MonoBehaviour
{
    #region Declarations
    MainDatabase Database => MainDatabase.instance;

    public List<RecipeSO> recipes;
    public GameObject menuPanel;
    #endregion

    #region Unity Methods
    private void Start()
    {
        foreach (var recipe in Database.unlockedRecipes)
        {
            if(recipe.isUnlocked)
            {
                recipes.Add(recipe);
            }
        }
    }
    #endregion

    #region Methods
    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
    #endregion
}
