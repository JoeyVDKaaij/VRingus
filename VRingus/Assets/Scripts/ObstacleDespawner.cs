using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null)
            return;
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.transform.parent.gameObject);
        }
        if (other.CompareTag("Room"))
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
