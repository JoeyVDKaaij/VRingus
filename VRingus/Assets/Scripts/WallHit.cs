using UnityEngine;

public class WallHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider) && other.CompareTag("MainCamera"))
        {
            other.GetComponentInChildren<PlayerHealth>().HitPlayer();
        }
    }
}
