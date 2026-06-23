using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IPointerDownHandler
{
    public static Deck instance;

    public List<SkillCard> startingCards;
    public Queue<SkillCard> deckQueue;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        for(int i = 0; i < startingCards.Count; i++)
        {
            int index = Random.Range(0, startingCards.Count);
            deckQueue.Enqueue(startingCards[index]);
            startingCards.RemoveAt(index);
        }
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        //open card inventory
    }
}
