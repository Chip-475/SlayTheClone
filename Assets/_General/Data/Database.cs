using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Database : MonoBehaviour
{
    #region Declarations
    public static Database instance;

    public List<Skill> skillPrefabs = new();
    public static Dictionary<string, Skill> skillTable = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        skillPrefabs.ForEach(s => skillTable.Add(s.data.id, s));
    }
    #endregion
}
