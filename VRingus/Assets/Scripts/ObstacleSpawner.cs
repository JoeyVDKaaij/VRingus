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
    private GameObject[] room = null;

    [Header("Spawn Settings")]
    [SerializeField, Tooltip("Set the requirement on how the GameObject spawns.")]
    private SpawnRequirement spawnRequirement = SpawnRequirement.Timed;

    [SerializeField, Range(5, 100), Tooltip("Set how long it should wait until it spawns the first new GameObject.")]
    private float startingSpawnDelay = 20f;
    [SerializeField, Range(1, 100), Tooltip("Set how long it should wait until it spawns a new GameObject.")]
    private float spawnRate = 1f;

    [SerializeField, Tooltip("Sets the offset position of the center of the collider in a gameobject.")]
    private Vector3 colliderPositionOffSet = new Vector3(0, 0, 0);
    [SerializeField, Tooltip("Set if the GameObject spawns on hit.")]
    private bool spawnOnEnter = false;

    //This variable keeps track of which room should be spawned
    private int roomCounter = 0;

    private GameObject previouslySpawnedRoom = null;


    private void Awake()
    {
        //initialPosition = transform.position;
        //transform.position += spawnPositionOffset;
        //newPosition = transform.position + spawnPositionOffset;

        gameObject.GetComponent<BoxCollider>().center += colliderPositionOffSet;
    }

    // Start is called before the first frame update
    void Start()
    {
        //checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<StopRoomAdvance>();
        StartSpawningObjects();
    }

    // Update is called once per frame
    void SpawnRandomObject()
    {
        if (room.Length > 0 && roomCounter <= room.Length - 1)
        {
            GameObject Room;
            RandomizeScript randomRoom = room[roomCounter].GetComponent<RandomizeScript>();

            //Random room choice
            if (room[roomCounter].GetComponent<RandomizeScript>() == null)
            {
                Room = room[roomCounter];
            }
            //Pre-defined choices
            else
            {
                Room = randomRoom.room[Random.Range(0, randomRoom.room.Length - 1)];
            }
            roomCounter++;
            GameObject instantiatedRoom = Instantiate(Room, transform.position, Quaternion.identity);
            if (previouslySpawnedRoom == null)
                previouslySpawnedRoom = instantiatedRoom;
            Collider roomCollider = previouslySpawnedRoom.GetComponent<Collider>();
            if (roomCollider != null && roomCounter != 0)
            {
                instantiatedRoom.transform.position += new Vector3(0, 0, roomCollider.bounds.size.z/2 + colliderPositionOffSet.z - 1);
                previouslySpawnedRoom = instantiatedRoom;
            }
        }
        else
        {
            if (roomCounter > room.Length - 1)
                return;
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
        if (other.CompareTag("Room") && !spawnOnEnter && spawnRequirement == SpawnRequirement.Collision)
        {
            SpawnRandomObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room") && spawnOnEnter && spawnRequirement == SpawnRequirement.Collision)
        {
            SpawnRandomObject();
        }
    }
}