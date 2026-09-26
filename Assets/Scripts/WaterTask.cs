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

    Interactor inter;
    AudioSource sfx;
    int hits = 0;
    bool used = false;

    void Start()
    {
        ShowStage(1);
        inter = FindFirstObjectByType<Interactor>();
        sfx = GetComponent<AudioSource>();
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

        if (inter == null || inter.carried != requiredTool || !inter.bucketFilled)
        {
            if (sfx != null && denySound != null) sfx.PlayOneShot(denySound);
            return;
        }

        hits++;
        if (hits == 1) ShowStage(2);
        if (hits == 2) ShowStage(3);
        if (sfx != null && waterSound != null) sfx.PlayOneShot(waterSound);

        if (hits >= hitsNeeded)
        {
            used = true;
            if (manager != null) manager.TaskDone(taskIndex);
            inter.bucketFilled = false;
            if (sfx != null && doneSound != null) sfx.PlayOneShot(doneSound);
        }
    }
}
