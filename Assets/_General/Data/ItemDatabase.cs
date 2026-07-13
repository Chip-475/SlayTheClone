using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    #region Declarations
    public static ItemDatabase instance;

    public Dictionary<string, ItemSO> itemTable = new();
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

        foreach (var item in Resources.LoadAll<ItemSO>("Items"))
        {
            itemTable.Add(item.id, item);
        }
    }
    #endregion

    #region Methods
    public static ItemSO GetItem(string id)
    {
        if(instance.itemTable.TryGetValue(id, out ItemSO item))
        {
            return item;
        }
        else
        {
            Debug.Log("Item not found.");
            return null;
        }
    }
    #endregion

    [System.Serializable]
    public class ItemSaveData
    {
        public string id;
        public int amount;
    }
}
