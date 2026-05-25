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
    public List<SkillCard> skillsInHand = new();
    public SkillCard selectedCard;
    public Enemy selectedTarget;

    public void AddCard(SkillCard skill)
    {
        var temp = CardCreator.instance.CreateCard(skill, splineContainer.transform.position, Quaternion.identity);
        skillsInHand.Add(temp);
        StartCoroutine(SetSkillPositions(0.2f));
    }
    public IEnumerator SetSkillPositions(float duration)
    {
        if(skillsInHand.Count == 0) yield break;

        float spacing = 1f / skillsInHand.Count;
        float firstPosition = 0.5f - (skillsInHand.Count - 1) * spacing / 2;
        Spline spline = splineContainer.Spline;

        for(int i = 0; i < skillsInHand.Count; i++)
        {
            float p = firstPosition + i * spacing;  
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            skillsInHand[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            skillsInHand[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration);
    }

    public void UseSkill(Enemy enemy)
    {

    }
}
