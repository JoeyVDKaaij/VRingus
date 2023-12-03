using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private MovingObstacle[] scripts;

    private bool buttonPressed = false;

    private void Start()
    {
        scripts = FindObjectsOfType<MovingObstacle>();
    }

    void Update()
    {
        if (buttonPressed)
        {
            foreach (MovingObstacle script in scripts)
            {
                script.CheckpointToggle();
            }
        }
    }
}
