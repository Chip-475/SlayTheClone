using UnityEngine;

public class SkillAnim : MonoBehaviour
{
    public Animator animator;
    public string animationName;

    private void Start()
    {
        animator.CrossFade(animationName, 0, 0);

        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name != animationName) continue;

            Destroy(gameObject, clip.length);
            return;
        }
    }

    public void Instantiate(Vector3 pos, string animName)
    {
        animationName = animName;
        Instantiate(this, pos, Quaternion.identity);
    }
}
