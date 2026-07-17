using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RecipeDatabase : MonoBehaviour
{
    #region Declarations
    public static RecipeDatabase instance;

    public Dictionary<string, RecipeSO> recipeTable = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        foreach (var recipe in Resources.LoadAll<RecipeSO>("Recipes"))
        {
            recipeTable.Add(recipe.id, recipe);
        }
    }
    #endregion

    #region Methods
    public static RecipeSO GetItem(string id)
    {
        if (instance.recipeTable.TryGetValue(id, out RecipeSO recipe))
        {
            return recipe;
        }
        else
        {
            Debug.Log("Recipe not found.");
            return null;
        }
    }
    public static List<RecipeSO> GetUnlockedRecipes()
    {
        return instance.recipeTable.Values
            .Where(recipe => recipe.isUnlocked)
            .ToList();
    }
    #endregion

    [System.Serializable]
    public class RecipeSaveData
    {
        public string id;
        public int amount;

        public bool isUnlocked;
    }
}
