using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class InventorySO : ScriptableObject
{
    public int money;

    public List<SkillCard> cards;
    public List<ItemSO> items;
}
