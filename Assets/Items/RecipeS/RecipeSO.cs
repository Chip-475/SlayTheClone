using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class RecipeSO : ScriptableObject
{
    [Serializable]
    public struct Ingredient
    {
        public ItemSO item;
        public int amount;
    }
    [Serializable]
    public struct Result
    {
        public ItemSO item;
        public int amount;
    }

    public int id;
    [Space]
    public Sprite icon;
    public string recipeName;
    [TextArea] public string desc = "A piece of paper with instructions on it. Maybe it could turn out useful.";
    [Space]
    public List<Ingredient> ingredients;
    public List<Result> results;
}
