using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using static MapManager;

public class Node : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Declarations
    [JsonIgnore] Database Database => Database.instance;
    [JsonIgnore] MapManager Manager => MapManager.instance;

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
    [Space]

    [Header("References")]
    [JsonIgnore] public SpriteRenderer spriteRenderer;

    [Header("Meta")]
    public int id;
    public bool connected;
    public List<int> forwardConnections = new();

    public bool isAssigned = false;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    #endregion

    #region Methods
    public void LoadType()
    {
        switch (type)
        {
            case NodeType.Null:
                spriteRenderer.color = Color.black;
                break;
            case NodeType.Entry:
                spriteRenderer.color = Color.white;
                break;
            case NodeType.Boss:
                spriteRenderer.color = Color.black;
                break;
            case NodeType.Battle:
                spriteRenderer.color = Color.red;
                break;
            case NodeType.EliteBattle:
                spriteRenderer.color = Color.violet;
                break;
            case NodeType.Shop:
                spriteRenderer.color = Color.green;
                break;
            case NodeType.Rest:
                spriteRenderer.color = Color.orange;
                break;
            case NodeType.Event:
                spriteRenderer.color = Color.yellow;
                break;
            case NodeType.Shortcut:
                spriteRenderer.color = Color.blue;
                break;
            default:
                break;
        }
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        List<int> nextNodes = new();
        GetNodeById(Manager.currentNodeId).forwardConnections.ForEach(id => { nextNodes.Add(id); });
        if (!nextNodes.Contains(id)) return;

        gameObject.transform.DOScale(0.8f, 0.15f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        List<int> nextNodes = new();
        GetNodeById(Manager.currentNodeId).forwardConnections.ForEach(id => { nextNodes.Add(id); });
        if (!nextNodes.Contains(id)) return;

        gameObject.transform.DOScale(0.5f, 0.15f);
    }
    public async void OnPointerClick(PointerEventData eventData)
    {
        List<int> nextNodes = new();
        GetNodeById(Manager.currentNodeId).forwardConnections.ForEach(id => { nextNodes.Add(id); });
        if (!nextNodes.Contains(id)) return;

        PlayerPrefs.SetInt("Current Node", id);
        string sceneToLoad = type.ToString();
        await MapManager.instance.fadePanel.GetComponent<CanvasGroup>().DOFade(1, 0.3f)
            .AsyncWaitForCompletion();
        await SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
    }
    #endregion

    #region Utilities
    public static IEnumerable<NodeType> NodeTypes()
    {
        yield return NodeType.EliteBattle;
        yield return NodeType.Event;
        yield return NodeType.Shop;
        yield return NodeType.Rest;   
    }

    public class SaveData
    {
        public int id;
        public List<int> forwardConnections = new();
        public SerializableVector3 position;
        public NodeType type;
    }
    public SaveData CompileData()
    {
        SaveData saveData = new()
        {
            id = id,
            forwardConnections = forwardConnections,
            position = new SerializableVector3(transform.position),
            type = type
        };

        return saveData;
    }
    #endregion
}

public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
