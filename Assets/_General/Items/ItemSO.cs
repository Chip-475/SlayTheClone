using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite Icon => sprite;

    public int id;
    [Space]
    public Sprite sprite;
    public string itemName;
    [TextArea] public string desc;
    [Space]
    public bool canSell;
    public int sellPrice;
    [Space]
    public int amount;
}
