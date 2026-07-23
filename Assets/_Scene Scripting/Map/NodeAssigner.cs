using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NodeAssigner : MonoBehaviour
{
    #region Declarations
    MainDatabase Database => MainDatabase.instance;

    public int nBattles;
    public int nEliteBattles;
    public int nEvents;
    public int nShops;
    public int nRests;
    public int nShortcuts;

    public IEnumerable<int> Values()
    {
        yield return nEliteBattles;
        yield return nEvents;
        yield return nShops;
        yield return nRests;
        yield return nShortcuts;
    }
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

#pragma warning disable
    public async Task AssignSpecialNodes()
    {
        // im not typing the entire explanation here
        // ask if you need

        var map = MapManager.instance.map;

        var nodeTypes = Node.NodeTypes().ToList();
        var nodeCurrentAmounts = Values().ToList();
        var nodeMaxAmounts = Database.MaxNodeAmounts().ToList();

        int cycles = nodeTypes.Count;
        for(int i = 0; i < cycles; i++)
        {
            int chanceToSpawn = 80;

            for (int j = 0; j < nodeTypes.Count; j++)
            {
                bool generate = Random.Range(0, 100) < chanceToSpawn;

                while (generate && nodeCurrentAmounts[j] < nodeMaxAmounts[j])
                {
                    int layerIndex = Random.Range(1, map.Keys.Count);
                    int nodeIndex = Random.Range(0, map[layerIndex].Count);

                    var node = map[layerIndex][nodeIndex];
                    if (node.isAssigned == false)
                    {
                        node.type = nodeTypes[j];
                        node.isAssigned = true;

                        nodeCurrentAmounts[j]++;
                        generate = false;
                    }
                }
            }
        }
    }
#pragma warning enable

    public async Task AssignBattles()
    {
        var map = MapManager.instance.map;

        foreach (var layer in map.Values)
        {
            foreach (var node in layer)
            {
                if(node.isAssigned == false)
                {
                    node.type = Node.NodeType.Battle;
                    nBattles++;
                }
                node.LoadType();
            }
        }
    }
    #endregion
}
