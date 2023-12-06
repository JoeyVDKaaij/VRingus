using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField, Tooltip("What the weapon shoots.")]
    private GameObject bullet;
    [SerializeField, Tooltip("How long the weapon has to wait until it can shoot again (in seconds).")]
    private float fireRate = 5.0f;
    [SerializeField, Tooltip("How close the player has to be in order for the weapon to rotate.")]
    private float distanceLookingTreshold = 20.0f;
    [SerializeField, Tooltip("Set to true if the weapon can shoot one bullet")]
    private bool shootOnce = false;
    private bool shoot = true;
    //[SerializeField, Tooltip("How far the weapon can angle away from the player before it doesn't shoot (-1 = completely different direction, 1 = staring directly at it)."), Range(-1,1)]
    //public float dotProductThreshold = 0.5f;
    //[SerializeField, Tooltip("How fast the weapon rotates (if the lookat function isn't used).")]
    //private float rotationSpeed = 1.0f;

    // Non inspector configurable variables
    private float timer = 0f;
    private GameObject player = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        timer += Time.deltaTime;


        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            
            if (distance < distanceLookingTreshold)
            {
                //Vector3 directionToPlayer = player.transform.position - transform.position;

                //Quaternion lookrotation = Quaternion.LookRotation(directionToPlayer);

                //transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * rotationSpeed);

                //transform.LookAt(player.transform);

                //Vector3 directionToOtherObject = player.transform.position - transform.position;
                //float dotProduct = Vector3.Dot(transform.forward, directionToOtherObject);

                if (timer > fireRate && !shootOnce ^ shootOnce && shoot)
                {
                    Instantiate(bullet, transform.position, transform.rotation);
                    timer = 0f;
                    shoot = false;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distanceLookingTreshold);
    }
}
}
