using UnityEngine;

public class LocatorArrow : MonoBehaviour
{
    void Start()
    {
        transform.position = MapManager.GetNodeById(PlayerPrefs.GetInt("Current Node")).transform.position;
    }
}
