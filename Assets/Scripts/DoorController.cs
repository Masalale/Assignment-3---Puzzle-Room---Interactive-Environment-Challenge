using UnityEngine;

// Opens the exit door when PuzzleManager completes all five tasks.
public class DoorController : MonoBehaviour
{
    public bool opened = false;
    public AudioClip openSound;

    public void Open()
    {
        if (opened) return;
        opened = true;

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && openSound != null) audio.PlayOneShot(openSound);

        transform.localPosition = new Vector3(-0.545f, 1.1f, 4.6f);
        transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
    }
}
