using UnityEngine;

public class WaterTask : MonoBehaviour
{
    public PuzzleManager manager;
    public int taskIndex = 0;
    public int hitsNeeded = 3;
    public Color wetColor = new Color(0.3f, 0.5f, 0.25f);
    public Renderer leaf;
    public AudioClip waterSound;
    public AudioClip doneSound;

    int hits = 0;
    bool used = false;

    void Interact()
    {
        if (used) return;

        hits++;
        if (leaf != null) leaf.transform.localScale = leaf.transform.localScale * 1.1f;

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && waterSound != null) audio.PlayOneShot(waterSound);

        if (leaf != null) leaf.material.color = wetColor;

        if (hits >= hitsNeeded)
        {
            used = true;
            if (manager != null) manager.TaskDone(taskIndex);
            AudioSource audio2 = GetComponent<AudioSource>();
            if (audio2 != null && doneSound != null) audio2.PlayOneShot(doneSound);
            Debug.Log("watered " + taskIndex);
        }
    }
}
