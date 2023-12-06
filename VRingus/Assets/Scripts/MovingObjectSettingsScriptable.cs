using UnityEngine;

[CreateAssetMenu(fileName = "MovingObstacleSettings", menuName = "Settings/MovingObstacleSettings", order = 1)]
public class MovingObstacleSettings : ScriptableObject
{
    [Header("Movement properties")]
    [Range(-200, 0)]
    public float movementSpeed = 1.0f;

    public Direction direction = Direction.x;
}