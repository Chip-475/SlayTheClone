using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public float X_Offset;
    public float Y_Offset;

    public Node entryNode;
    public List<Node> nodes = new();

    public int seed;
    public int seedAttempts;

    public int numberOfLayers;
    public int numberOfPages;
    public int numberOfNodes;
    public int generationAttempts;

    int nEliteBattle, nShop, nRest, nEvent, nShortCut;
    bool finalShop = false, finalRest = false, finalEvent = false,finalFinal=false;
    void Start()
    {
        seed = Random.Range(0, int.MaxValue);
        Random.InitState(seed);
        nodes = GenerateMap();
        GenerateConnection();
        DrawConnection();
    }
    public List<Node> GenerateMap()
    {
        List<Node> nodes = new();
        numberOfLayers = Random.Range(5, 8);
        numberOfPages = 3;
        int nodeID = 1;
        SpawnNode(nodes, (int)Node.NodeType.Entry, -1, 0, 2);
        for (int i = 0; i < numberOfLayers; i++)
        {
            numberOfNodes = Random.Range(2, 5);
            int order = 0;
            for (int j = 0; j < numberOfNodes; j++)
            {
                int whatToSpawn;
                if (i == numberOfLayers - 1 && !finalFinal)
                {
                    if (numberOfNodes < 3)
                    {
                        numberOfNodes = 3;
                    }
                    whatToSpawn = Random.Range(0, 3);
                    if (whatToSpawn == 0 && !finalShop)
                    {
                        SpawnNode(nodes, (int)Node.NodeType.Shop, i, nodeID, order);
                        nodeID++;
                        order++;
                        finalShop = true;
                    }
                    else if (whatToSpawn == 1 && !finalRest)
                    {
                        SpawnNode(nodes, (int)Node.NodeType.Rest, i, nodeID, order);
                        nodeID++;
                        order++;
                        finalRest = true;
                    }
                    else if (whatToSpawn == 2 && !finalEvent)
                    {
                        SpawnNode(nodes, (int)Node.NodeType.Event, i, nodeID, order);
                        nodeID++;
                        order++;
                        finalEvent = true;
                    }
                    else
                    {
                        j--;
                    }
                    if(finalShop && finalRest && finalEvent)
                    {
                        finalFinal = true;
                    }
                    continue;
                }//last layer
                whatToSpawn = Random.Range(3, 9);
                if (CanSpawn(whatToSpawn, i, numberOfNodes))
                {
                    SpawnNode(nodes, whatToSpawn, i, nodeID, order);
                    SetNumber(whatToSpawn);
                    order++;
                    nodeID++;
                }
                else
                {
                    j--;
                }
            }
        }
        SpawnNode(nodes, (int)Node.NodeType.Boss, numberOfLayers, nodeID, 2);
        nodes.Sort((a, b) => a.layerId.CompareTo(b.layerId));
        return nodes;
    }
    public bool CanSpawn(int type, int layer, int numberOfNodes)
    {
        generationAttempts++;
        Debug.Log("Try to spawn:" + generationAttempts);
        if (generationAttempts > 100)
        {
            generationAttempts = 0;
            Reseed();
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Rest && layer == 0)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Event && layer == 0)
        {
            return false;
        }
        if((Node.NodeType)type == Node.NodeType.EliteBattle && nEliteBattle >= numberOfLayers / 2)
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
        if ((Node.NodeType)type == Node.NodeType.Shop && nShop >= 3)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Rest && nRest >= 1)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Event && nEvent >= 2)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Shortcut && nShortCut >= 2)
        {
            return false;
        }
        if ((Node.NodeType)type == Node.NodeType.Shortcut && layer==numberOfLayers-1)
        {
            return false;
        }
        return true;
    }
    public void SpawnNode(List<Node> nodes, int whatToSpawn, int layer, int nodeID, int row)
    {
        float x, y;
        x = (layer + 1) * X_Offset;
        Vector2 pos = new(0, 0);
        GameObject node = Instantiate(nodePrefab, pos, Quaternion.identity);
        Node component = node.GetComponent<Node>();
        component.nodeId = nodeID;
        component.layerId = layer;
        component.row = row;
        component.normalizedRow = row - (numberOfNodes - 1) / 2;
        component.type = (Node.NodeType)whatToSpawn;
        if (component.type == Node.NodeType.Entry || component.type == Node.NodeType.Boss)
        {
            y = 0;
            component.normalizedRow = 0;
        }
        else
        {
            y = (row - (numberOfNodes - 1) / 2f) * Y_Offset;
        }
        pos = new Vector2(x, y);
        node.transform.position = pos;
        nodes.Add(component);
    }
    public void GenerateConnection()
    {
        Debug.Log(seedAttempts + "attempts to generate");
        for (int i = 0; i <= numberOfLayers; i++)
        {
            foreach (Node current in nodes)
            {
                if (current.layerId != i) continue;
                if (current.layerId == 0)
                {
                    current.toConnect.Add(entryNode.nodeId);
                    continue;
                }
                List<Node> possibleConnection = new();
                foreach (Node prev in nodes)
                {
                    if (prev.layerId != i - 1) continue;
                    if (Mathf.Abs(current.normalizedRow - prev.normalizedRow) <= 1 || current.type == Node.NodeType.Boss)
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
                bool hasExit = false;
                foreach (Node current in nodes)
                {
                    if (current.layerId == i && current.toConnect.Contains(prev.nodeId))
                    {
                        hasExit = true;
                        break;
                    }
                }
                if (!hasExit)
                {
                    List<Node> possibleTargets = new();
                    foreach (Node current in nodes)
                    {
                        if (current.layerId != i) continue;
                        if (prev.type == Node.NodeType.Entry || current.type == Node.NodeType.Boss || Mathf.Abs(prev.normalizedRow - current.normalizedRow) <= 1)
                        {
                            possibleTargets.Add(current);
                        }
                    }
                    if (possibleTargets.Count > 0)
                    {
                        Node target = possibleTargets[Random.Range(0, possibleTargets.Count)];
                        target.toConnect.Add(prev.nodeId);
                    }
                }
            }
            foreach (Node current in nodes) 
            { 
                if (current.layerId != i) continue;
                if (current.layerId == 0) continue;
                bool hasEntry = false;
                foreach (Node prev in nodes) 
                { 
                    if (current.toConnect.Contains(prev.nodeId)) 
                    { 
                        hasEntry = true; 
                        break;
                    } 
                } 
                if (!hasEntry)
                { 
                    List<Node> possibleSources = new();
                    foreach (Node prev in nodes)
                    {
                        if (prev.layerId != i - 1) continue; 
                        if (prev.type == Node.NodeType.Entry || current.type == Node.NodeType.Boss || Mathf.Abs(prev.normalizedRow - current.normalizedRow) <= 1)
                        {
                            possibleSources.Add(prev);
                        } 
                    }
                    if (possibleSources.Count > 0)
                    {
                        Node source = possibleSources[Random.Range(0, possibleSources.Count)];
                        current.toConnect.Add(source.nodeId);
                    }
                }
            }
        }
    }
    public void SetNumber(int spawned)
    {
        Node.NodeType type = (Node.NodeType)spawned;
        switch (type)
        {
            case Node.NodeType.EliteBattle:
                nEliteBattle++;
                break;
            case Node.NodeType.Shop:
                nShop++;
                break;
            case Node.NodeType.Rest:
                nRest++;
                break;
            case Node.NodeType.Event:
                nEvent++;
                break;
            case Node.NodeType.Shortcut:
                nShortCut++;
                break;
        }
    }
    public void DrawConnection()
    {
        foreach (Node node in nodes)
        {
            foreach (int id in node.toConnect)
            {
                Node target = nodes.Find(x => x.nodeId == id);
                if (target == null) { Debug.LogWarning("Node not found: " + id); continue; }
                GameObject lnObj = new("Line");
                LineRenderer lineRenderer = lnObj.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, node.transform.position);
                lineRenderer.SetPosition(1, target.transform.position);
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
                lineRenderer.sortingOrder = -1;
            }
        }
    }
    public void Reseed()
    {
        Debug.Log("Reseeding, old seed:" + seed);
        seedAttempts++;
        seed = Random.Range(0, int.MaxValue);
        Random.InitState(seed);
        foreach (Node node in nodes)
        {
            Destroy(node.gameObject);
        }
        nodes.Clear();
        if (seedAttempts < 10)
        {
            nodes = GenerateMap();
            GenerateConnection();
        }
        else
        {
            Debug.LogError("Failed to generate map after 10 attempts");
        }
    }
}