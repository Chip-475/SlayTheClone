using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeGenerator))]
[RequireComponent(typeof(NodeAssigner))]
public class MapManager : MonoBehaviour
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

    public static MapManager instance;

    public NodeGenerator nodeGenerator;
    public NodeAssigner nodeAssigner;

    public Node startingNode;
    public Node bossNode;
    public Node nodePrefab;
    public Dictionary<int, List<Node>> map;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;

        nodeGenerator = GetComponent<NodeGenerator>();
        nodeAssigner = GetComponent<NodeAssigner>();
    }
    private void Start()
    {
        nodeGenerator.InitLayerKeys();
        nodeGenerator.SpawnNodes();
        nodeGenerator.PositionNodes();
        map = nodeGenerator.layers;

        nodeAssigner.AssignSpecialNodes();
    }
    #endregion
}
