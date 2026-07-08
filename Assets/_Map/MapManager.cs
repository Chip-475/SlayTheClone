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

    public Dictionary<int, List<Node>> layers;
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
        layers = nodeGenerator.layers;

        nodeAssigner.AssignEliteBattles();
    }
    #endregion
}
