using UnityEngine;

[CreateAssetMenu(fileName = "Mob Drop", menuName = "Scriptable Objects/Mob Drop")]
public class MobDrop : ScriptableObject
{
    public int id;
    [Space]
    public Sprite icon;
    public string itemName;
    public string desc;
    [Space]
    public bool canSell;
    public int sellPrice;
    [Space]
    public int amount;
}
