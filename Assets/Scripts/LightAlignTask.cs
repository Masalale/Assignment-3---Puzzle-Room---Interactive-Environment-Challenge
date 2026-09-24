using UnityEngine;

public class LightAlignTask : MonoBehaviour
{
    public PuzzleManager manager;
    public int taskIndex = 2;
    public int turnsNeeded = 2;
    public bool needsOthersFirst = false;
    public int needCount = 4;
    public Light growLight;
    public Color doneColor = Color.green;
    public AudioClip turnSound;
    public AudioClip doneSound;
    public AudioClip denySound;

    int turns = 0;
    bool used = false;

    void Interact()
    {
        if (used) return;

        if (needsOthersFirst && manager != null && manager.Count() < needCount)
        {
            Debug.Log("not yet - finish others first");
            AudioSource denyAudio = GetComponent<AudioSource>();
            if (denyAudio != null && denySound != null) denyAudio.PlayOneShot(denySound);
            return;
        }

        turns++;
        transform.Rotate(0, 90, 0);
        AudioSource turnAudio = GetComponent<AudioSource>();
        if (turnAudio != null && turnSound != null) turnAudio.PlayOneShot(turnSound);

        if (turns >= turnsNeeded)
        {
            used = true;
            if (growLight != null)
            {
                growLight.color = doneColor;
                growLight.intensity = 2f;
            }
            if (manager != null) manager.TaskDone(taskIndex);
            AudioSource doneAudio = GetComponent<AudioSource>();
            if (doneAudio != null && doneSound != null) doneAudio.PlayOneShot(doneSound);
            Debug.Log("aligned " + taskIndex);
        }
    }
}
