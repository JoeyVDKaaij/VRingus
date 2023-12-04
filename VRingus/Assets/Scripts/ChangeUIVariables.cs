using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeUIVariables : MonoBehaviour
{
    [Header("Drag the player in here")]
    [SerializeField, Tooltip("In this case the player should be on the MainCamera")]
    private GameObject Player;

    private PlayerHealth playerHealth;
    private ScoreSystem playerScore;
    private int lives;
    private int score;

    //Variables from this script
    [SerializeField] private TextMeshProUGUI livesText;
    //[SerializeField] private TextMeshProUGUI scoreText;
    //[SerializeField] private TextMeshProUGUI timeText;

    private void Awake()
    {
        if (!Player.CompareTag("MainCamera"))
        {
            Debug.LogError("The player must be the MainCamera!");
        }
        else
        {
            playerHealth = Player.GetComponent<PlayerHealth>();
            playerScore = Player.GetComponent<ScoreSystem>();
        }
    }

    void setText(TextMeshProUGUI displayText, string content, float number)
    {
        displayText.text = content + $": {number}";
    }

    // Update is called once per frame
    void Update()
    {
        lives = playerHealth.Health;
        score = playerScore.Score;
        setText(livesText, "Lives", lives);
        //setText(scoreText, "Room", score);
        //timeText.text = "Time: "+GameManager.gameManager.TimeInFormat();
    }
}
