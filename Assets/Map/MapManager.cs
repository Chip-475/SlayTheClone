using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public float X_Offset;
    public float Y_Offset;

    public Node entryNode;
    public List<Node> nodes = new();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public List<Node> GenerateMap()
    {
        List<Node> map = new();

        for (int i = 0; i < 4; i++)
        {

        }

        return map;
    }
}
