using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField, Tooltip("How many hitpoints the player has.")]
    private int health;
    [SerializeField, Tooltip("How long the player can't be damaged for.")]
    private float hitDelay = 5;
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (health == 0)
            GameOver();
    }

    public void HitPlayer()
    {
        if (timer > hitDelay)
        {
            health--;
            timer = 0.0f;
        }
    }

    void GameOver()
    {
        //Game Over Script
    }
}
