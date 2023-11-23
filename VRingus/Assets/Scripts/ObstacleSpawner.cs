using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField, Tooltip("Sets which GameObjects it should spawn.")]
    private GameObject[] obstacles = null;
    [SerializeField, Tooltip("Sets room GameObjects that should spawn.")]
    private GameObject room = null;
    [SerializeField, Range(1, 100), Tooltip("Set how long it should wait until it spawns tjhe first new GameObject.")]
    private float startingSpawnDelay;
    [SerializeField, Range(1, 100), Tooltip("Set how long it should wait until it spawns a new GameObject.")]
    private float spawnRate;
    [SerializeField, Tooltip("Set the spawnOffSet")]
    private Vector3 spawnPositionOffset = Vector3.zero;
    private Vector3 newPosition = Vector3.zero;
    private Vector3 initialPosition = Vector3.zero;

    private void Awake()
    {
        initialPosition = transform.position;
        transform.position += spawnPositionOffset;
        newPosition = transform.position + spawnPositionOffset;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSpawningObjects();
    }

    // Update is called once per frame
    void SpawnRandomObject()
    {
        if (obstacles.Length > 0)
        {
            newPosition = initialPosition + spawnPositionOffset;
            transform.position = newPosition;
            Instantiate(obstacles[Random.Range(0, obstacles.Length-1)], transform);
            if (room != null)
                Instantiate(room, transform);
        }
        else
        {
            Debug.LogWarning("No GameObjects found in GameObject Array");
        }
    }

    void StartSpawningObjects()
    {
        InvokeRepeating("SpawnRandomObject", startingSpawnDelay, spawnRate);
    }

    void StopSpawningObjects()
    {
        CancelInvoke("SpawnRandomObject");
    }
}
