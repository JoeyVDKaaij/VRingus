using System.Collections;
using UnityEngine;

public class SelfDestroyerScript : MonoBehaviour
{
    [Header("Destroy Settings")]
    [SerializeField, Tooltip("Set the amount of time until the GameObject gets destroyed (in seconds).")]
    private float destroyCountDown = 2.0f;

    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(destroyCountDown);
        Destroy(gameObject);
    }
}
