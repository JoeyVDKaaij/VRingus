using UnityEngine;

public class TriggerDoorAnimation : MonoBehaviour
{
    [SerializeField, Tooltip("Distance at which the player gets detected")]
    private float distanceToPlayer = 3.0f;
    private Transform player;
    private Animator animator;

    bool doorIsOpen = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= distanceToPlayer)
        {
            if (!doorIsOpen)
            {
                Debug.Log("Opening door...");
                animator.SetBool("DoorOpen", true);
                doorIsOpen = true;
            }
        }
        else
        {
            if (doorIsOpen)
            {
                Debug.Log("Closing door...");
                animator.SetBool("DoorOpen", false);
                doorIsOpen = false;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distanceToPlayer);
    }
}
