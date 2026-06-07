using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Rendering;
using System.Xml.Linq;

public class MapManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public Transform map;


    public float X_Offset;
    public float Y_Offset;
    public Transform topLeft;
    public Transform bottomRight;


    public Node entryNode;
    public List<Node> nodes = new();
    public LineRenderer ln;
    public int seed;
    public int numberOfLayers;
    public int numberOfPages;
    public int numberOfNodes;
    public int tries;
    public int attempts;
    int nBattle, nEliteBattle, nShop, nRest, nEvent, nShortCut;
    bool finalShop = false, finalRest = false, finalEvent = false,finalFinal=false;
    void Start()
    {
        seed = Random.Range(0, int.MaxValue);
        Random.InitState(seed);
        nodes = GenerateMap();
        generateConnection();
        drawConnection();
    }
    public List<Node> GenerateMap()
    {
        List<Node> nodes = new();
        numberOfLayers = Random.Range(5, 8);
        numberOfPages = 3;
        int nodeID = 1;
        calcOffset();
        SpawnNode(nodes, (int)Node.NodeType.Entry, -1, 0, 2);
        for (int i = 0; i < numberOfLayers; i++)
        {
            numberOfNodes = Random.Range(2, 5);
            int order = 0;
            for (int j = 0; j < numberOfNodes; j++)
            {
                int whatToSpawn;
                if (i == numberOfLayers - 1&&!finalFinal)
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
                    setNumber(whatToSpawn);
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
        tries++;
        Debug.Log("Try to spawn:" + tries);
        if (tries > 100)
        {
            tries = 0;
            reseed();
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
        x =topLeft.position.x + (layer + 1) * X_Offset;
        Vector2 pos = new Vector2(0, 0);
        GameObject node = Instantiate(nodePrefab, pos, Quaternion.identity,map);
        Node component = node.GetComponent<Node>();
        component.nodeId = nodeID;
        component.layerId = layer;
        component.row = row;
        component.normalizedRow = row - (numberOfNodes - 1) / 2;
        component.type = (Node.NodeType)whatToSpawn;
        if (component.type == Node.NodeType.Entry || component.type == Node.NodeType.Boss)
        {
            y = (topLeft.position.y + bottomRight.position.y) / 2f;
            component.normalizedRow = 0;
        }
        else
        {
            
            y =((topLeft.position.y+bottomRight.position.y)/2f) + (row - (numberOfNodes - 1) / 2f) * Y_Offset;
        }
        pos = new Vector2(x, y);
        node.transform.position = pos;
        nodes.Add(component);
    }
    public void generateConnection()
    {
        Debug.Log(attempts + "attempts to generate");
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
                    if (prev.layerId == i - 2 && prev.type == Node.NodeType.Shortcut)
                    {
                        if (Mathf.Abs(current.normalizedRow - prev.normalizedRow) <= 1)
                        {
                            if (Random.Range(0, 2) == 0)
                            {
                                current.toConnect.Add(prev.nodeId);
                            }
                        }
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
    public void setNumber(int spawned)
    {
        Node.NodeType type = (Node.NodeType)spawned;
        switch (type)
        {
            case Node.NodeType.Battle:
                nBattle++;
                break;
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
    public void drawConnection()
    {
        foreach (Node node in nodes)
        {
            foreach (int id in node.toConnect)
            {
                Node target = nodes.Find(x => x.nodeId == id);
                if (target == null) { Debug.LogWarning("Node not found: " + id); continue; }
                GameObject lnObj = new GameObject("Line");
                lnObj.transform.parent = map;
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
    public void calcOffset()
    {
        float width=bottomRight.position.x - topLeft.position.x;
        float height=topLeft.position.y - bottomRight.position.y;
        X_Offset = width / (numberOfLayers + 1);
        Y_Offset = height /5;
    }
    public void reseed()
    {
        Debug.Log("Reseeding,old seed:" + seed);
        attempts++;
        seed = Random.Range(0, int.MaxValue);
        Random.InitState(seed);
        foreach (Node node in nodes)
        {
            Destroy(node.gameObject);
        }
        nodes.Clear();
        if (attempts < 10)
        {
            nodes = GenerateMap();
            generateConnection();
        }
        else
        {
            Debug.LogError("Failed to generate map after 10 attempts");
        }
    }
}