using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public int id;
    [Space]
    public Sprite icon;
    public string itemName;
    [TextArea] public string desc;
    [Space]
    public bool canSell;
    public int sellPrice;
    [Space]
    public int amount;
}
