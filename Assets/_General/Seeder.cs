using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class Seeder : MonoBehaviour
{
    public static Seeder instance;
    public static int seed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        seed = Random.Range(0, int.MaxValue);

        Random.InitState(seed);
    }
}
