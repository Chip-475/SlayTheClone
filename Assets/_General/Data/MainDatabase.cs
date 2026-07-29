using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class MainDatabase : MonoBehaviour
{
    #region Declarations
    public static MainDatabase instance;

    public int nStartingCards;
    public int nCardsAtTurnStart;
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
    #endregion
}
