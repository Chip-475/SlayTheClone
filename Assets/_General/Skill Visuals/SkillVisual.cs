using UnityEngine;

public class SkillVisual : MonoBehaviour
{
    public Animator animator;
    public string animationName;

    private void Start()
    {
        animator.CrossFade(animationName, 0, 0);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Destroy(gameObject, stateInfo.length);
    }

    public void Instantiate(Vector3 pos, string animName)
    {
        animationName = animName;
        Instantiate(this, pos, Quaternion.identity);
    }
}
