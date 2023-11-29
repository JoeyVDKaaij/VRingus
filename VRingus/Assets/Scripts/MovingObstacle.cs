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

    public void CheckpointCheck()
    {
        Debug.Log("Changing checkpoint status in MovingObstacle...");
        isAtCheckpoint = !isAtCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAtCheckpoint)
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
    }
}

