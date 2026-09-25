using UnityEngine;

public class WaterTask : MonoBehaviour
{
    public PuzzleManager manager;
    public int taskIndex = 0;
    public int hitsNeeded = 3;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public AudioClip waterSound;
    public AudioClip doneSound;
    public AudioClip denySound;
    public string requiredTool = "hose";

    int hits = 0;
    bool used = false;

    void Start()
    {
        ShowStage(1);
    }

    void ShowStage(int n)
    {
        if (stage1 != null) stage1.SetActive(n == 1);
        if (stage2 != null) stage2.SetActive(n == 2);
        if (stage3 != null) stage3.SetActive(n == 3);
    }

    void Interact()
    {
        if (used) return;

        Interactor inter = FindFirstObjectByType<Interactor>();
        if (inter == null || inter.carried != requiredTool || !inter.bucketFilled)
        {
            AudioSource denyAudio = GetComponent<AudioSource>();
            if (denyAudio != null && denySound != null) denyAudio.PlayOneShot(denySound);
            Debug.Log("need a filled bucket first");
            return;
        }

        hits++;
        if (hits == 1) ShowStage(2);
        if (hits == 2) ShowStage(3);

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && waterSound != null) audio.PlayOneShot(waterSound);

        if (hits >= hitsNeeded)
        {
            used = true;
            if (manager != null) manager.TaskDone(taskIndex);
            if (inter != null) inter.bucketFilled = false;
            AudioSource audio2 = GetComponent<AudioSource>();
            if (audio2 != null && doneSound != null) audio2.PlayOneShot(doneSound);
            Debug.Log("watered " + taskIndex);
        }
    }
}
