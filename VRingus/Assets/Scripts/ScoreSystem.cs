using UnityEngine;

//This script should be placed on a GameManager script in general. 
//However, for the sake of simplicity for now it lies on the player (MainCamera component in this case).

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(PlayerHealth))]
public class ScoreSystem : MonoBehaviour
{
    [Header("Score gain")]
    [SerializeField, Tooltip("Set how much score the player would get per obstacle dodged"), Min(1)] private int scoreGain = 1;
    private int score;
    private PlayerHealth player;
    private Collider col;

    public int Score { get { return score; } }

    private void Awake()
    {
        player = GetComponent<PlayerHealth>();
        col = GetComponent<BoxCollider>();

        // Check if the collider is a trigger collider
        if (!col.isTrigger)
        {
            Debug.LogError("This script requires a trigger collider!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            //Apply the amount of scoreToGain to the current player score
            score += scoreGain;
        }
    }
}
