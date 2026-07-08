using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NodeAssigner : MonoBehaviour
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

    public int nBattles;
    public int nEliteBattles;
    public int nEvents;
    public int nShops;
    public int nRests;
    public int nShortcuts;
    #endregion

    #region Unity Methods

    #endregion

    #region Methods
    public void AssignEliteBattles()
    {
        // Max 1 per layer

        int chanceToSpawn = 60;

        for (int i = 1; i < Database.bossLayer - 1; i++)
        {
            var nodes = MapManager.instance.layers[i];
            bool generateBattle = Random.Range(0, 100) < chanceToSpawn;

            if (generateBattle)
            {
                int index = Random.Range(0, nodes.Count);

                nodes[index].type = Node.NodeType.EliteBattle;
            }
        }
    }
    public void AssignEvents()
    {

    }
    #endregion
}
