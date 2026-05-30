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
    public List<CardSkill> cardsInHand = new();

    public void AddCard(CardSkill skill)
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
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cardsInHand[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            cardsInHand[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration);
    }
}
