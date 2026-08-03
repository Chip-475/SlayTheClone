using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using DG.Tweening;

[RequireComponent(typeof(NodeGenerator))]
[RequireComponent(typeof(NodeAssigner))]
[RequireComponent(typeof(NodeConnecter))]
public class MapManager : MonoBehaviour
{
    #region Declarations
    public Database Database => Database.instance;

    public static MapManager instance;

    public NodeGenerator nodeGenerator;
    public NodeAssigner nodeAssigner;
    public NodeConnecter nodeConnecter;
    [Space]
    public Node startingNode;
    public Node bossNode;
    public Node nodePrefab;
    [Space]
    public int minNodesPerLayer;
    public int maxNodesPerLayer;
    public int bossLayer;
    public int maxEliteBattles;
    public int maxEvents;
    public int maxShops;
    public int maxRests;
    public int maxShortcuts;
    [Space]
    public float nodeOffsetX;
    public float nodeOffsetY;
    [Space]
    public Dictionary<int, List<Node>> map;
    public List<Node> nodeLookupTable;
    public int currentNodeId;
    public string mapPath;
    [Space]
    public Stack<GameObject> menuHistory;
    public GameObject fadePanel;
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
        fadePanel.SetActive(true);
        fadePanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f);

        mapPath = Path.Combine(Application.persistentDataPath, "map.json");

        if (PlayerPrefs.GetInt("generated") == 0)  // false
        {
            await Generate();
        }
        else
        {
            LoadMap();
            FillNodeTable();
            nodeConnecter.DrawConnections();

            startingNode.gameObject.SetActive(false);
            bossNode.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Methods
    public void SaveMap()
    {
        var data = new Dictionary<int, List<Node.SaveData>>();

        foreach (var key in map.Keys)
        {
            List<Node.SaveData> nodeData = new();
            foreach(var node in map[key])
            {
                nodeData.Add(node.CompileData());
            }

            data.Add(key, nodeData);
        }
        PlayerPrefs.SetInt("Current Node", currentNodeId);

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

            for(int i = 0; i < data.Keys.Count; i++)
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

                map.Add(i, nodes);
            }

            currentNodeId = PlayerPrefs.GetInt("Current Node");
        }
    }
    public void FillNodeTable()
    {
        nodeLookupTable.Add(startingNode);

        foreach (var layer in map.Keys)
        {
            foreach(var node in map[layer])
            {
                nodeLookupTable.Add(node);
            }
        }

        nodeLookupTable.Add(bossNode);
    }

    [ContextMenu("ReGenerate Map")]
    public async Task Generate()
    {
        foreach (var layer in map.Keys)
        {
            foreach(var node in map[layer])
            {
                Destroy(node.gameObject);
            }
        }
        map = new();

        nodeGenerator.InitLayerKeys();
        nodeGenerator.SpawnNodes();
        nodeGenerator.PositionNodes();
        map = nodeGenerator.layers;

        await nodeAssigner.AssignSpecialNodes();
        await nodeAssigner.AssignBattles();

        nodeConnecter.AssignConnections();
        FillNodeTable();
        nodeConnecter.DrawConnections();

        currentNodeId = 0;
        SaveMap();
        PlayerPrefs.SetInt("generated", 1); // set true

        startingNode.gameObject.SetActive(false);
        bossNode.gameObject.SetActive(false);
    }
    

    #endregion

    #region Utilities
    public IEnumerable<int> MaxNodeAmounts()
    {
        yield return maxEliteBattles;
        yield return maxEvents;
        yield return maxShops;
        yield return maxRests;
        yield return maxShortcuts;
    }
    public static Node GetNodeById(int id)
    {
        foreach (var layer in instance.map.Keys)
        {
            foreach (var node in instance.map[layer])
            {
                if (node.id == id) return node;
            }
        }

        return null;
    }
    #endregion
}
