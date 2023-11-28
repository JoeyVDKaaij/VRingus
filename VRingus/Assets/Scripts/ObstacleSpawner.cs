using UnityEditor.Rendering;
using UnityEngine;

public enum SpawnRequirement
{
    Timed,
    Collision
}

public class ObstacleSpawner : MonoBehaviour
{
    [Header("GameObject Settings")]
    [SerializeField, Tooltip("Sets room GameObjects that should spawn. (MANDATORY)")]
    private GameObject room = null;
    [SerializeField, Tooltip("Sets door GameObjects that should spawn. (MANDATORY)")]
    private GameObject door = null;
    [SerializeField, Tooltip("Sets the position of the door related to the center of the spawner. (Set this before playing)")]
    private Vector3 doorPositionOffset = new Vector3(0,0,0);

    [Header("Position Settings")]
    [SerializeField, Tooltip("Set the spawnOffSet")]
    private Vector3 spawnPositionOffset = Vector3.zero;
    private Vector3 newPosition = Vector3.zero;
    private Vector3 initialPosition = Vector3.zero;

    [Header("Spawn Settings")]
    [SerializeField, Tooltip("Set the requirement on how the GameObject spawns.")]
    private SpawnRequirement spawnRequirement = SpawnRequirement.Timed;

    [SerializeField, Range(1, 100), Tooltip("Set how long it should wait until it spawns tjhe first new GameObject.")]
    private float startingSpawnDelay = 20f;
    [SerializeField, Range(1, 100), Tooltip("Set how long it should wait until it spawns a new GameObject.")]
    private float spawnRate = 1f;

    [SerializeField, Tooltip("Sets the size of the spawner collider with the size of a gameobject.")]
    private MeshRenderer colliderSize = null;
    [SerializeField, Tooltip("Sets the offset position of the center of the collider in a gameobject.")]
    private Vector3 colliderPositionOffSet = new Vector3(0,0,0);
    [SerializeField, Tooltip("Set if the GameObject spawns on hit.")]
    private bool spawnOnHit = false;

    private void Awake()
    {
        initialPosition = transform.position;
        transform.position += spawnPositionOffset;
        newPosition = transform.position + spawnPositionOffset;

        if (colliderSize != null)
            gameObject.GetComponent<BoxCollider>().size = colliderSize.bounds.size;

        gameObject.GetComponent<BoxCollider>().center += colliderPositionOffSet;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSpawningObjects();
    }

    // Update is called once per frame
    void SpawnRandomObject()
    {
        if (room != null && door != null)
        {
            newPosition = initialPosition + spawnPositionOffset;
            transform.position = newPosition;
            Instantiate(room, transform);
            GameObject gameObject = Instantiate(door, transform);
            gameObject.transform.position += doorPositionOffset;
        }
        else
        {
            Debug.LogWarning("No GameObjects found in GameObject Array");
        }
    }

    void StartSpawningObjects()
    {
        switch (spawnRequirement)
        {
            case SpawnRequirement.Timed:
                InvokeRepeating("SpawnRandomObject", startingSpawnDelay, spawnRate);
                break;

            case SpawnRequirement.Collision:
                SpawnRandomObject();
                break;

            default:
                Debug.LogWarning("spawnRequirement is not initialized or is set to NULL!");
                break;
        }
    }

    void StopSpawningObjects()
    {
        if (spawnRequirement == SpawnRequirement.Timed)
        {
            CancelInvoke("SpawnRandomObject");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Room") && !spawnOnHit && spawnRequirement == SpawnRequirement.Collision) SpawnRandomObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room") && spawnOnHit && spawnRequirement == SpawnRequirement.Collision) SpawnRandomObject();
    }
}