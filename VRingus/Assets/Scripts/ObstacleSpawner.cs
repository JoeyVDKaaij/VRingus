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

    [Header("Position Settings")]
    [SerializeField, Tooltip("Set the spawnOffSet")]
    private Vector3 spawnPositionOffset = Vector3.zero;
    private Vector3 newPosition = Vector3.zero;
    private Vector3 initialPosition = Vector3.zero;

    [Header("Spawn Settings")]
    [SerializeField, Tooltip("Set the requirement on how the GameObject spawns.")]
    private SpawnRequirement spawnRequirement = SpawnRequirement.Timed;

    [SerializeField, Range(5, 100), Tooltip("Set how long it should wait until it spawns the first new GameObject.")]
    private float startingSpawnDelay = 20f;
    [SerializeField, Range(1, 100), Tooltip("Set how long it should wait until it spawns a new GameObject.")]
    private float spawnRate = 1f;

    [SerializeField, Tooltip("Sets the size of the spawner collider with the size of a gameobject.")]
    private MeshRenderer colliderSize = null;
    [SerializeField, Tooltip("Sets the offset position of the center of the collider in a gameobject.")]
    private Vector3 colliderPositionOffSet = new Vector3(0, 0, 0);
    [SerializeField, Tooltip("Set if the GameObject spawns on hit.")]
    private bool spawnOnEnter = false;

    [Header("The first set of random rooms")]
    [SerializeField] private GameObject[] randomSet1 = null;

    [Header("The second set of random rooms")]
    [SerializeField] private GameObject[] randomSet2 = null;

    //This variable keeps track of which room should be spawned
    private int roomCounter = 0;

    private bool randomSpawn = false;
    private int randomSetCounter = 0;

    private bool canExit = true;
    //private StopRoomAdvance checkpoint; //The checkpoint that makes the whole room advance pause

    private int frameCounter = 6;


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
        //checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<StopRoomAdvance>();
        StartSpawningObjects();
    }

    // Update is called once per frame
    void SpawnRandomObject()
    {
        if (room.Length > 0 && roomCounter <= room.Length - 1)
        {
            GameObject Room;
            // randomRoom = room[roomCounter].GetComponent<RandomizeScript>();
            #region RandomRoom
            //Random room choice
            //if (room[roomCounter].GetComponent<RandomizeScript>() == null)
            //{
            //    Room = randomRoom.room[Random.Range(0,randomRoom.room.Length-1)];
            //    roomCounter++;
            //}
            #endregion
            #region Pre-defined
            //Pre-defined choices
            Room = room[roomCounter];
            roomCounter += 1;
            Instantiate(Room, transform.position, Quaternion.identity);
            #endregion
            //Animator anim = Room.GetComponentInChildren<Animator>();
            //anim.SetTrigger("CloseDoor");
            //checkpoint.AddRoom(Room);
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