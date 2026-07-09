using UnityEngine;
using System.Linq;

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
    private void Start()
    {
        MapManager.instance.startingNode.type = Node.NodeType.Entry;
        MapManager.instance.bossNode.type = Node.NodeType.Boss;

        MapManager.instance.startingNode.isAssigned = true;
        MapManager.instance.bossNode.isAssigned = true;
    }
    #endregion

    #region Methods
    public void AssignSpecialNodes()
    {
        var map = MapManager.instance.map;

        var nodeTypes = Node.NodeTypes();
        var nodeMaxAmounts = Database.MaxNodeAmounts();

        foreach (var typeAmountPair in nodeTypes.Zip(nodeMaxAmounts, (type, maxAmount) => new { type, maxAmount }))
        {
            int chanceToSpawn = 50;

            for (int i = 0; i < typeAmountPair.maxAmount; i++)
            {
                bool generate = Random.Range(0, 100) < chanceToSpawn;

                while (generate)
                {

                    int layerIndex = Random.Range(1, map.Keys.Count);
                    int nodeIndex = Random.Range(0, map[layerIndex].Count);

                    var node = map[layerIndex][nodeIndex];
                    if (node.isAssigned == false)
                    {
                        node.type = typeAmountPair.type;
                        node.isAssigned = true;

                        generate = false;
                    }
                }
            }
        }
    }

    public void AssignEliteBattles()
    {
        // Max 1 per layer

        var map = MapManager.instance.map;

        int chanceToSpawn = 50;
        
        for(int i = 0; i < Database.maxEliteBattles; i++)
        {
            bool generate = Random.Range(0, 100) < chanceToSpawn;

            while (true)
            {
                if (generate)
                {
                    int layerIndex = Random.Range(1, map.Keys.Count);
                    int nodeIndex = Random.Range(0, map[layerIndex].Count);

                    var node = map[layerIndex][nodeIndex];
                    if (node.isAssigned == false)
                    {
                        node.type = Node.NodeType.EliteBattle;
                        node.isAssigned = true;
                        nEliteBattles++;
                        break;
                    }
                    else continue;
                }
            }
        }
    }
    public void AssignEvents()
    {
        // Max 1 per layer

        var map = MapManager.instance.map;

        int chanceToSpawn = 60;

        for (int i = 0; i < Database.maxEvents; i++)
        {
            bool generate = Random.Range(0, 100) < chanceToSpawn;

            while (true)
            {
                if (generate)
                {
                    int layerIndex = Random.Range(1, map.Keys.Count);
                    int nodeIndex = Random.Range(0, map[layerIndex].Count);

                    var node = map[layerIndex][nodeIndex];
                    if (node.isAssigned == false)
                    {
                        node.type = Node.NodeType.Event;
                        node.isAssigned = true;
                        nEvents++;
                        break;
                    }
                    else continue;
                }
            }
        }
    }
    public void AssignShops()
    {
        // Max 1 per layer

        var map = MapManager.instance.map;

        int chanceToSpawn = 60;

        for (int i = 0; i < Database.maxShops; i++)
        {
            bool generate = Random.Range(0, 100) < chanceToSpawn;

            while (true)
            {
                if (generate)
                {
                    int layerIndex = Random.Range(1, map.Keys.Count);
                    int nodeIndex = Random.Range(0, map[layerIndex].Count);

                    var node = map[layerIndex][nodeIndex];
                    if (node.isAssigned == false)
                    {
                        node.type = Node.NodeType.Shop;
                        node.isAssigned = true;
                        nShops++;
                        break;
                    }
                    else continue;
                }
            }
        }
    }
    public void AssignRests()
    {
        // Max 1 per layer

        var map = MapManager.instance.map;

        int chanceToSpawn = 60;

        for (int i = 0; i < Database.maxRests; i++)
        {
            bool generate = Random.Range(0, 100) < chanceToSpawn;

            while (true)
            {
                if (generate)
                {
                    int layerIndex = Random.Range(1, map.Keys.Count);
                    int nodeIndex = Random.Range(0, map[layerIndex].Count);

                    var node = map[layerIndex][nodeIndex];
                    if (node.isAssigned == false)
                    {
                        node.type = Node.NodeType.Rest;
                        node.isAssigned = true;
                        nRests++;
                        break;
                    }
                    else continue;
                }
            }
        }
    }
    public void AssignShortcuts()
    {
        // Max 1 per layer

        var map = MapManager.instance.map;

        int chanceToSpawn = 60;

        for (int i = 0; i < Database.maxShortcuts; i++)
        {
            bool generate = Random.Range(0, 100) < chanceToSpawn;

            while (true)
            {
                if (generate)
                {
                    int layerIndex = Random.Range(1, map.Keys.Count);
                    int nodeIndex = Random.Range(0, map[layerIndex].Count);

                    var node = map[layerIndex][nodeIndex];
                    if (node.isAssigned == false)
                    {
                        node.type = Node.NodeType.Shortcut;
                        node.isAssigned = true;
                        nShortcuts++;
                        break;
                    }
                    else continue;
                }
            }
        }
    }
    #endregion
}
