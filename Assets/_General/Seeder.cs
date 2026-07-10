using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class Seeder : MonoBehaviour
{
    public static int seed;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        seed = Random.Range(0, int.MaxValue);

        Random.InitState(seed);
    }
}
