using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IPointerDownHandler
{
    #region Declarations
    public List<SkillCard> startingCards;
    public Queue<SkillCard> deckQueue = new();

    public Transform deckTransform;
    #endregion

    #region Methods
    public void FillDeck()
    {
        for (int i = 0; i < startingCards.Count; i++)
        {
            int index = Random.Range(0, startingCards.Count);
            deckQueue.Enqueue(startingCards[index]);
            startingCards.RemoveAt(index);
        }
    }

    /// <summary>
    /// Draws nToDraw cards from the deck and adds them to the hand.
    /// </summary>
    public void DrawCards(int nToDraw)
    {
        for (int i = 0; i < nToDraw; i++)
        {
            var card = Instantiate(deckQueue.Dequeue(), deckTransform.position, Quaternion.identity);

            CombatManager.instance.hand.cardsInHand.Add(card);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //open card inventory
    }
    #endregion
}