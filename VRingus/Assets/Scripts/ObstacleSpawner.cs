using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField, Tooltip("Sets which GameObjects it should spawn.")]
    private GameObject[] obstacles = null;
    [SerializeField, Range(1, 100), Tooltip("Set how long it should wait until it spawns a new GameObject.")]
    private float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObject", spawnRate, spawnRate);
    }

    // Update is called once per frame
    void SpawnRandomObject()
    {
        if (obstacles.Length > 0)
        {
            Instantiate(obstacles[Random.Range(0, obstacles.Length-1)], transform);
        }
        else
        {
            Debug.LogWarning("No GameObjects found in GameObject Array");
        }
    }
}
