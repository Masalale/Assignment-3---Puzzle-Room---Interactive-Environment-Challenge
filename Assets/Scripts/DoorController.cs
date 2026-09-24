using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool opened = false;
    public AudioClip openSound;
    public float openDelay = 2f;

    public void Open()
    {
        if (opened) return;
        opened = true;
        Invoke("Slide", openDelay);
    }

    void Slide()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && openSound != null) audio.PlayOneShot(openSound);

        // slide door up so player can walk out
        transform.position = transform.position + new Vector3(0, 2.2f, 0);
        Debug.Log("door opened");
    }
}
