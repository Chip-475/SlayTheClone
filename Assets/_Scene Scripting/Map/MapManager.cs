using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeGenerator))]
[RequireComponent(typeof(NodeAssigner))]
public class MapManager : MonoBehaviour
{
    #region Declarations
    MainDatabase Database => MainDatabase.instance;

    public static MapManager instance;

    public NodeGenerator nodeGenerator;
    public NodeAssigner nodeAssigner;
    public NodeConnecter nodeConnecter;

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
        nodeConnecter= GetComponent<NodeConnecter>();
    }
    private async void Start()
    {
        nodeGenerator.InitLayerKeys();
        nodeGenerator.SpawnNodes();
        nodeGenerator.PositionNodes();
        map = nodeGenerator.layers;

        await nodeAssigner.AssignSpecialNodes();
        await nodeAssigner.AssignBattles();
    }
    #endregion
}
