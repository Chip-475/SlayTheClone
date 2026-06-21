using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using System.Linq;

public class Hand : MonoBehaviour
{
    public static Hand instance;

    public List<SkillCard> cardsInHand = new();
    public List<Transform> cardSlots = new();
    public Transform deckPos;

    private void Start()
    {
        instance = this;

        DrawCards(Globals.instance.db.startingCardsCount);
    }

    public void DrawCards(int nToDraw)
    {
        // Pick out cards at random from the deck
        List<SkillCard> cardsToAdd = new();
        for (int i = 0; i < nToDraw; i++)
        {
            // to modify once deck is created
            var cardToAddIndex = Random.Range(0, Globals.instance.db.skillPrefabs.Count);
            var card = Instantiate(Globals.instance.db.skillPrefabs[cardToAddIndex], deckPos.position, Quaternion.identity);
            cardsToAdd.Add(card);
        }

        AddCard(cardsToAdd.ToArray());
    }
    public void AddCard(params SkillCard[] cards)
    {
        foreach(var card in cards)
        {
            cardsInHand.Add(card);
        }

        Organize();
    }
    public void Organize()
    {
        for(int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].transform.DOMove(cardSlots[i].position, 0.2f);
        }
    }
}
