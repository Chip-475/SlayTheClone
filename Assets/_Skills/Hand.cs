using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using System.Linq;

public class Hand : MonoBehaviour
{
    #region Declarations
    public List<SkillCard> cardsInHand = new();
    public List<Transform> cardSlots = new();
    public Transform deckPos;
    #endregion

    #region Methods
    public void Organize()
    {
        for(int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].transform.DOMove(cardSlots[i].position, 0.2f);
        }
    }
    #endregion
}
