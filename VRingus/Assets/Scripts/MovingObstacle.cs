
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
    [SerializeField, Tooltip("Where (from this GameObject position) should the target be.")]
    private Vector3 targetPositionOffset = new Vector3(0, 0, 0);

    [SerializeField, Tooltip("When should the room slow down and stop moving from the set target position.")]
    private float stoppingDistance = 5.0f;

    [SerializeField, Tooltip("Set to true if this GameObject is the final room")]
    private bool finalRoom = false;

    private Transform target = null;

    private Transform player;

    private bool stopping;
    private bool stopped;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new GameObject().transform;
        target.position = transform.position + targetPositionOffset;
    }

    public void CheckpointCheck()
    {
        Debug.Log("Changing checkpoint status in MovingObstacle...");
        isAtCheckpoint = !isAtCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
        target.position = transform.position + targetPositionOffset;
        float distance = Vector3.Distance(player.position, target.position);
        if (!isAtCheckpoint && !finalRoom || !isAtCheckpoint && distance < stoppingDistance && finalRoom && !stopping)
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
        else stopping = true;

        if (!isAtCheckpoint && finalRoom && stopping && !stopped)
        {
            float decelerationFactor = Mathf.Clamp01(distance / stoppingDistance);

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

            if (decelerationFactor < 0.2) stopped = true;
        }
    }
}