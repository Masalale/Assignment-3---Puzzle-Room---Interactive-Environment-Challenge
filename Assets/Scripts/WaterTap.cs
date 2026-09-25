using UnityEngine;

public class WaterTap : MonoBehaviour
{
    public AudioClip pourSound;
    public bool isPouring = false;

    AudioSource audioSource;
    Interactor interactor;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        interactor = FindFirstObjectByType<Interactor>();
    }

    void Interact()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (interactor == null) interactor = FindFirstObjectByType<Interactor>();

        isPouring = !isPouring;

        if (isPouring)
        {
            if (interactor != null && interactor.carried == "hose" && !interactor.bucketFilled)
            {
                interactor.bucketFilled = true;
            }

            if (audioSource != null && pourSound != null)
            {
                audioSource.clip = pourSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource != null) audioSource.Stop();
        }
    }
}
