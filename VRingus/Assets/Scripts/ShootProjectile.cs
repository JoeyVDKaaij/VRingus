using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField, Tooltip("Place the projectile prefab here")]
    private GameObject projectile;

    private void Awake()
    {
        if(projectile == null)
        {
            Debug.LogError($"No projectile found! Please provide a projectile prefab to cannon: {gameObject}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider) && other.gameObject.CompareTag("MainCamera"))
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}
