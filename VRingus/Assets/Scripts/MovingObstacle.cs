using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    private enum Direction
    {
        x,
        y,
        z
    }

    [Header("Movement properties")]
    [SerializeField, Range(-200, 200), Tooltip("The speed in which it travels. Negative number moves the object backwards.")]
    private float movementSpeed = 1.0f;
    [SerializeField, Tooltip("Which axis it moves in.")]
    private Direction direction = Direction.x;

    // Update is called once per frame
    void Update()
    {
        switch (direction) 
        {
            case Direction.x:
                transform.Translate(new Vector3(Time.deltaTime * movementSpeed, 0, 0));
                break;
            case Direction.y:
                transform.Translate(new Vector3(0, Time.deltaTime * movementSpeed, 0));
                break;
            case Direction.z:
                transform.Translate(new Vector3(0, 0, Time.deltaTime * movementSpeed));
                break;
            default:
                Debug.LogWarning("Cannot find Enum value.");
                break;
        }
    }
}
