using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpeakerTrigger : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (source.clip != null && other.CompareTag("MainCamera"))
        {
            source.Play();
        }
    }
}
