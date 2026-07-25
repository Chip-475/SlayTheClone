using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEditor.Timeline;
using System.Linq;

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
    public string mapPath;
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
         //PlayerPrefs.SetInt("generated", 0); // testing purposes

        mapPath = Path.Combine(Application.persistentDataPath, "map.json");

        if (PlayerPrefs.GetInt("generated") == 0)  // false
        {
            nodeGenerator.InitLayerKeys();
            nodeGenerator.SpawnNodes();
            nodeGenerator.PositionNodes();
            map = nodeGenerator.layers;

            await nodeAssigner.AssignSpecialNodes();
            await nodeAssigner.AssignBattles();

            nodeConnecter.AssignConnections();

            SaveMap();
            PlayerPrefs.SetInt("generated", 1); // set true
        }
        else
        {
            LoadMap();
        }
    }
    #endregion

    #region Methods
    public void SaveMap()
    {
        var data = new Dictionary<int, List<Node.SaveData>>();

        foreach (var key in map.Keys)
        {
            if (key == 0) continue;

            List<Node.SaveData> nodeData = new();
            foreach(var node in map[key])
            {
                nodeData.Add(node.CompileData());
            }

            data.Add(key, nodeData);
        }

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(mapPath, json);
    }
    public void LoadMap()
    {
        if (File.Exists(mapPath))
        {
            string json = File.ReadAllText(mapPath);
            var data = JsonConvert.DeserializeObject<Dictionary<int, List<Node.SaveData>>>(json);
            map = new();

            for(int i = 1; i < data.Keys.Count; i++)
            {
                List<Node> nodes = new();

                foreach(var nodeData in data[i])
                {
                    var node = Instantiate(nodePrefab);

                    node.id = nodeData.id;
                    node.forwardConnections.AddRange(nodeData.forwardConnections);
                    node.transform.position = nodeData.position.ToVector3();
                    node.type = nodeData.type;
                    node.LoadType();

                    nodes.Add(node);
                }
            }
        }
    }
    #endregion
}
