using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Rendering;

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
    int nBattle, nEliteBattle, nShop, nRest, nEvent, nShortCut;
    void Start()
    {
        seed = Random.Range(0, int.MaxValue);
        Random.InitState(seed);
        nodes = GenerateMap();
        generateConnection();
    }

    void Update()
    {
        
    }

    public List<Node> GenerateMap()
    {
        List<Node> nodes = new();
        numberOfLayers = Random.Range(5, 8);
        numberOfPages = 3;
        int nodeID = 1;
        SpawnNode(nodes,(int)Node.NodeType.Entry, -1, 0, 2);
        for (int i = 0; i < numberOfLayers; i++)
        {
            numberOfNodes= Random.Range(2, 5);
            int order = 0;
            for(int j = 0;j< numberOfNodes; j++)
            {
                int whatToSpawn;
                if(i==numberOfLayers-1)
                {
                    bool finalShop = false, finalRest = false, finalEvent = false;
                    while (!finalShop && !finalRest && !finalEvent)
                    {
                        whatToSpawn=Random.Range(0, 3);
                        if (whatToSpawn == 0)
                        {
                            SpawnNode(nodes, (int)Node.NodeType.Shop, i, nodeID,order);
                            nodeID++;
                            order++;
                            finalShop = true;
                        }
                        else if (whatToSpawn == 1)
                        {
                            SpawnNode(nodes, (int)Node.NodeType.Rest, i, nodeID,order);
                            nodeID++;
                            order++;
                            finalRest = true;
                        }
                        else if (whatToSpawn == 2)
                        {
                            SpawnNode(nodes, (int)Node.NodeType.Event, i, nodeID,order);
                            nodeID++;
                            order++;
                            finalEvent = true;
                        }
                    }
                    j = 2;
                    if (numberOfNodes <= 3) continue;
                }//last layer
                whatToSpawn= Random.Range(3, 9);
                if (CanSpawn(whatToSpawn, i, numberOfNodes))
                {
                    SpawnNode(nodes,whatToSpawn, i, nodeID,order);
                    order++;
                    nodeID++;
                }
                else
                {
                    j--;
                }
            }
        }
        SpawnNode(nodes,(int)Node.NodeType.Boss, numberOfLayers, nodeID,2);
        nodes.Sort((a, b) => a.layerId.CompareTo(b.layerId));
        return nodes;
    }
    public bool CanSpawn(int type, int layer, int numberOfNodes)
    {
        if ((Node.NodeType)type == Node.NodeType.Rest && layer == 0)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Event && layer == 0)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Shop && numberOfNodes < 3)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Rest && numberOfNodes < 3)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Shop && nShop >= 4)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Rest && nRest >= 2)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Event && nEvent >= 3)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Shortcut && nShortCut >= 2)
        {
            return false;
        }
        return true;
    }
    public void SpawnNode(List<Node> nodes,int whatToSpawn, int layer, int nodeID,int row)
    {
        Vector2 pos= new Vector2(Random.Range(0,5),Random.Range(0,5));
        GameObject node = Instantiate(nodePrefab,pos,Quaternion.identity);
        node.GetComponent<Node>().nodeId = nodeID;
        node.GetComponent<Node>().layerId = layer;
        node.GetComponent<Node>().row = row;
        node.GetComponent<Node>().type = (Node.NodeType)whatToSpawn;
        nodes.Add(node.GetComponent<Node>());
    }
    public void generateConnection()
    {
        for (int i = 0; i <= numberOfLayers; i++) {
            foreach (Node current in nodes)
            {
                if (current.layerId == i) continue;
                if (current.layerId == 0)
                {
                    current.toConnect.Add(entryNode.nodeId);
                    continue;
                }
                List<Node> possibleConnection = new();
                foreach (Node prev in nodes)
                {
                    if (prev.layerId != i-1) continue;
                    if (Mathf.Abs(current.row - prev.row) <= 1 || current.type == Node.NodeType.Boss)
                    {
                        possibleConnection.Add(prev);
                    }
                }
                foreach (Node possible in possibleConnection)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        current.toConnect.Add(possible.nodeId);
                    }
                }
                if (current.toConnect.Count == 0 && possibleConnection.Count > 0)
                {
                    current.toConnect.Add(possibleConnection[Random.Range(0, possibleConnection.Count)].nodeId);
                }
            }
            foreach (Node prev in nodes)
            {
                if (prev.layerId != i - 1) continue;
                bool exit=false;
                foreach(Node current in nodes)
                {
                    if(current.layerId == i && current.toConnect.Contains(prev.nodeId))
                    {
                        exit = true;
                        break;
                    }
                }
                if (!exit)
                {
                    List<Node> possibleTargets = new();
                    foreach (Node current in nodes)
                    {
                        if (current.layerId != i) continue;
                        if (prev.type == Node.NodeType.Entry ||Mathf.Abs(prev.row - current.row) <= 1)
                        {
                            possibleTargets.Add(current);
                        }
                    }
                    if (possibleTargets.Count > 0)
                    {
                        Node target = possibleTargets[Random.Range(0, possibleTargets.Count)];
                        target.toConnect.Add(prev.nodeId);
                        target.isConnected = true;
                    }
                }
            }
        }
    }
}
