using UnityEngine;

public class Node : MonoBehaviour
{
    public enum NodeType
    {
        Null,
        Entry,
        Battle,
        EliteBattle,
        Boss,
        Shop,
        Rest
    }
    public NodeType type = NodeType.Null;

    public int layerId;
    public int nodeId;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
