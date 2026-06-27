using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using NUnit.Framework;

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
        for (int i = startingCards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // UnityEngine.Random
            (startingCards[i], startingCards[j]) = (startingCards[j], startingCards[i]);
        }

        foreach (var card in startingCards)
        {
            deckQueue.Enqueue(card);
        }
    }

    /// <summary>
    /// Draws nToDraw cards from the deck and adds them to the hand.
    /// </summary>
    public void DrawCards(int nToDraw)
    {
        for (int i = 0; i < nToDraw; i++)
        {
            if(deckQueue.TryDequeue(out SkillCard cardToDraw))
            {
                var drawnCard = Instantiate(cardToDraw, deckTransform.position, Quaternion.identity);

                CombatManager.instance.hand.cardsInHand.Add(drawnCard);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //open card inventory
    }
    #endregion
}