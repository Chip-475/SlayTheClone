using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerEnterHandler, IPointerExitHandler
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
    public SceneManager sceneManager;
    public int layerId;
    public int nodeId;
    public int row;
    public List<int> toConnect = new();
    public int normalizedRow; //row - (numberOfNodes - 1) / 2
    public bool isConnected;
    private bool isHoveredOn = false;
    void Start()
    {
        switch (type)
        {
            case NodeType.Null:
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case NodeType.Entry:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case NodeType.Boss:
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case NodeType.Battle:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case NodeType.EliteBattle:
                gameObject.GetComponent<SpriteRenderer>().color = Color.violet;
                break;
            case NodeType.Shop:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case NodeType.Rest:
                gameObject.GetComponent<SpriteRenderer>().color = Color.orange;
                break;
            case NodeType.Event:
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case NodeType.Shortcut:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
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
        gameObject.transform.DOScale(1.2f, 0.15f);
        isHoveredOn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHoveredOn) return;
        gameObject.transform.DOScale(1f, 0.15f);
        isHoveredOn = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isHoveredOn) return;
    }
}
