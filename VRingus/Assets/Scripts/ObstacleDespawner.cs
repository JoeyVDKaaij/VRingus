using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacles"))
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
