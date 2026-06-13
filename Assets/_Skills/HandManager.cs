using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;
using System.Collections;
using DG.Tweening;
using UnityEditor.Rendering;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;
    private void Start()
    {
        instance = this;
    }

    public SplineContainer splineContainer;
    [Space]
    public List<SkillCard> cardsInHand = new();

    public void AddCard(SkillCard skill)
    {
        var temp = CardCreator.instance.CreateCard(skill, splineContainer.transform.position, Quaternion.identity);
        cardsInHand.Add(temp);
        SetCards(0.15f);
    }
    public void SetCards(float duration)
    {
        StartCoroutine(SetCardsCR(duration));
    }
    public IEnumerator SetCardsCR(float duration)
    {
        if(cardsInHand.Count == 0) yield break;

        float spacing = 1f / cardsInHand.Count;
        float firstPosition = 0.5f - (cardsInHand.Count - 1) * spacing / 2;
        Spline spline = splineContainer.Spline;

        for(int i = 0; i < cardsInHand.Count; i++)
        {
            float p = firstPosition + i * spacing;  
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Vector3 position = splinePosition + transform.position + 0.01f * i * Vector3.back;
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cardsInHand[i].transform.DOMove(position, duration);
            cardsInHand[i].transform.DORotate(rotation.eulerAngles, duration);

            cardsInHand[i].basePos = position;
            cardsInHand[i].baseRot = rotation;
        }

        yield return new WaitForSeconds(duration);
    }

    public void HideCards(bool show)
    {
        if(show)
        {
            foreach(var card in cardsInHand)
            {
                card.transform.DOMove(splineContainer.transform.position, 0.15f);
                card.transform.DOScale(0, 0.15f);
            }
        }
        else
        {
            foreach(var card in cardsInHand)
            {
                card.transform.DOMove(card.basePos, 0.15f);
                card.transform.DOScale(1, 0.15f);
            }
        }
    }
}
