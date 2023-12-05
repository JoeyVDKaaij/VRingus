using UnityEngine;

public enum Direction
{
    x,
    y,
    z
}
public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is where you add the scriptable object settings")]
    protected MovingObstacleSettings obstacleSettings;

    private bool isAtCheckpoint = false;

    public bool IsAtCheckpoint { get { return isAtCheckpoint; } }

    public MovingObstacleSettings Settings { get { return obstacleSettings; } }

    [Header("Stopping Movement Settings")]
    [SerializeField, Tooltip("Set to true if this GameObject is the final room")]
    private bool finalRoom = false;
    [SerializeField, Tooltip("Where (from this GameObject position) should the target be.")]
    private Vector3 targetPositionOffset = new Vector3(0, 0, 0);

    [SerializeField, Tooltip("When should the room slow down and stop moving from the set target position.")]
    private float stoppingDistance = 5.0f;


    private Vector3 target = new Vector3(0,0,0);
    private BoxCollider col;
    private float colliderLength;

    private Transform player;

    private bool stopping;
    private bool stopped;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        colliderLength = col.bounds.size.z;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = transform.position + targetPositionOffset;
    }

    public bool CheckpointCheck()
    {
        Debug.Log("Checking checkpoint status in MovingObstacle...");
        return isAtCheckpoint;
    }

    public void CheckpointToggle()
    {
        Debug.Log("Changing checkpoint status in MovingObstacle...");
        isAtCheckpoint = !isAtCheckpoint;
    }

    private void Deceleration(float decelerationFactor)
    {
        if (decelerationFactor >= 0.1) 
        {
            switch (obstacleSettings.direction)
            {
                case Direction.x:
                    transform.Translate(new Vector3(Time.deltaTime * obstacleSettings.movementSpeed * decelerationFactor, 0, 0));
                    break;
                case Direction.y:
                    transform.Translate(new Vector3(0, Time.deltaTime * obstacleSettings.movementSpeed * decelerationFactor, 0));
                    break;
                case Direction.z:
                    transform.Translate(new Vector3(0, 0, Time.deltaTime * obstacleSettings.movementSpeed * decelerationFactor));
                    break;
                default:
                    Debug.LogWarning("Cannot find Enum value.");
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.position + new Vector3(0,0,colliderLength/2);

        float distance = Vector3.Distance(player.position, target);
  
        if ((!isAtCheckpoint && !finalRoom) || (!isAtCheckpoint && distance > stoppingDistance && finalRoom && !stopping && !stopped))
        {
            switch (obstacleSettings.direction)
            {
                case Direction.x:
                    transform.Translate(new Vector3(Time.deltaTime * obstacleSettings.movementSpeed, 0, 0));
                    break;
                case Direction.y:
                    transform.Translate(new Vector3(0, Time.deltaTime * obstacleSettings.movementSpeed, 0));
                    break;
                case Direction.z:
                    transform.Translate(new Vector3(0, 0, Time.deltaTime * obstacleSettings.movementSpeed));
                    break;
                default:
                    Debug.LogWarning("Cannot find Enum value.");
                    break;
            }
        }
        else if(distance < stoppingDistance)
            stopping = true;

        if (!isAtCheckpoint && finalRoom && stopping && !stopped)
        {
            float decelerationFactor = Mathf.Clamp01(distance / stoppingDistance);
            Deceleration(decelerationFactor);

            if (decelerationFactor < 0.1) 
                stopped = true;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(target, stoppingDistance);
    }
}