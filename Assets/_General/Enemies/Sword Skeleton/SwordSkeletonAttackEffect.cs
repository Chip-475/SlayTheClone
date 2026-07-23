using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

#pragma warning disable
public class SwordSkeletonAttackEffect : MonoBehaviour
{
    public float destroyTime;

    private void Start()
    {
        _ = Anim();
        Destroy(gameObject, destroyTime);
    }

    async Task Anim()
    {
        while (this != null)
        {
            var basePos = transform.position;
            await transform.DOMove(new Vector2(basePos.x + 0.1f, basePos.y), 0.1f)
                .AsyncWaitForCompletion();
            await transform.DOMove(new Vector2(basePos.x - 0.1f, basePos.y), 0.1f)
                .AsyncWaitForCompletion();
            Anim();
        }
    }
}
