using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Declarations
    DatabaseSO Database => DB.instance.database;

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
    public SpriteRenderer spriteRenderer;
    [Header("Meta")]
    public int layerId;
    public int nodeId;
    public int row;
    public List<int> nodesToConnect = new();
    public int normalizedRow; //row - (numberOfNodes - 1) / 2
    public bool isConnected;
    public bool isAssigned = false;
    bool isHoveredOn = false;
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
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (type)
        {
            case NodeType.Boss:
                //sceneManager.LoadSceneAsync("battle");
                break;
            case NodeType.Battle:
                //sceneManager.LoadSceneAsync("battle");
                break;
            case NodeType.EliteBattle:
                //sceneManager.LoadSceneAsync("battle");
                break;
            case NodeType.Shop:
                //sceneManager.LoadSceneAsync("shop");
                break;
            case NodeType.Rest:
                //sceneManager.LoadSceneAsync("rest");
                break;
            case NodeType.Event:
                //sceneManager.LoadSceneAsync("event");
                break;
            case NodeType.Shortcut:
                //sceneManager.LoadSceneAsync("shortcut");
                break;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHoveredOn) return;
        gameObject.transform.DOScale(0.8f, 0.15f);
        isHoveredOn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHoveredOn) return;
        gameObject.transform.DOScale(0.5f, 0.15f);
        isHoveredOn = false;
    }
    #endregion

    #region Utilities
    public static IEnumerable<NodeType> NodeTypes()
    {
        yield return NodeType.EliteBattle;
        yield return NodeType.Event;
        yield return NodeType.Shop;
        yield return NodeType.Rest;   
        yield return NodeType.Shortcut;
    }
    #endregion
}
