using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour
{
    public static CardCreator instance;
    private void Start()
    {
        instance = this;
    }

    public CardSkill CreateCard(CardSkill cardPrefab, Vector2 position, Quaternion rotation)
    {
        CardSkill card = Instantiate(cardPrefab, position, rotation);
        card.transform.localScale = Vector3.zero;
        card.transform.DOScale(Vector3.one, 0.15f);
        return card;
    }
}
