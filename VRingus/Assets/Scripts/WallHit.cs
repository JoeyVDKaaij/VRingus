using UnityEngine;

public class WallHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            other.GetComponentInChildren<PlayerHealth>().HitPlayer();
        }
    }
}
