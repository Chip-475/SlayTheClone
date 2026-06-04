using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public enum NodeType
    {
        Null,
        Entry,
        Boss, //finalLayer
        Battle, //default case
        EliteBattle, //layer/2
        Shop,  //max 3, forced before boss,cant spawn on layer with <3 nodes
        Rest, // max 2, not on layer1,forced before boss,cant spawn on layer with <3 nodes
        Event,//max 3,not on layer1, forced before boss
        Shortcut //max 2
    }
    public NodeType type = NodeType.Null;

    public int layerId;
    public int nodeId;
    public int row;
    public List<int> toConnect = new();
    public int normalizedRow; //row - (numberOfNodes - 1) / 2
    public bool isConnected;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
