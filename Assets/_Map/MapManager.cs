using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;

public class MapManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public float X_Offset;
    public float Y_Offset;

    public Node entryNode;
    public List<Node> nodes = new();
    public int seed;
    public int numberOfLayers;
    public int numberOfPages;
    public int numberOfNodes;
    void Start()
    {
        seed = Random.Range(0, int.MaxValue);
        Random.InitState(seed);
    }

    void Update()
    {
        
    }

    public List<Node> GenerateMap()
    {
        List<Node> map = new();
        List<Node> layers= new();
        List<Node> nodes = new();
        numberOfLayers = Random.Range(5, 8);
        numberOfPages = 3;
        int nodeID = 0;
        int nBattle, nEliteBattle, nShop, nRest, nEvent, nShortCut;
        for(int i = 0; i < numberOfLayers; i++)
        {
            numberOfNodes= Random.Range(2, 5);
            for(int j = 0;j< numberOfNodes; j++)
            {
                GameObject node=Instantiate(nodePrefab);
                node.AddComponent<Node>();
                nodes.Add(node.GetComponent<Node>());
                nodes[j].nodeId = nodeID;
                nodeID++;
                while (true)
                {
                    int whatToSpawn = Random.Range(0, 6);
                    if (i == 0)
                    {
                        if (whatToSpawn != 3 && whatToSpawn != 4)
                        {
                            break;
                        }
                    } //notLayer1
                    if (i == numberOfLayers - 1)
                    {
                        whatToSpawn= Random.Range(2, 5);

                    }
                } 
                

            }
        }

        return map;
    }
}
