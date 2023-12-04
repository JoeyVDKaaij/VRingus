using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SelfDestroyerScript : MonoBehaviour
{
    [Header("Destroy Settings")]
    [SerializeField, Tooltip("Set the amount of time until the GameObject gets destroyed (in seconds).")]
    private float destroyCountDown = 2.0f;
    

    private AudioSource explosionSound;
    private float explosionTime = 0f;

    private void Awake()
    {
        explosionSound = GetComponent<AudioSource>();
        explosionTime = explosionSound.clip.length;
    }


    void Start()
    {
        StartCoroutine(DestroyCoroutine(destroyCountDown));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetType() == typeof(SphereCollider) && other.CompareTag("MainCamera"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            if(explosionSound.clip != null)
            {
                explosionSound.Play();
            }
            StopCoroutine(DestroyCoroutine(destroyCountDown));
            StartCoroutine(DestroyCoroutine(explosionTime));
        }
    }

    IEnumerator DestroyCoroutine(float countdown)
    {
        yield return new WaitForSeconds(countdown);
        Destroy(gameObject);
    }
}
