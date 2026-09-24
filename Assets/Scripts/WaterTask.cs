using UnityEngine;

public class WaterTask : MonoBehaviour
{
    public PuzzleManager manager;
    public int taskIndex = 0;
    public int hitsNeeded = 3;

    int hits = 0;
    bool used = false;

    void Interact()
    {
        if (used) return;

        hits++;
        transform.localScale = transform.localScale * 1.1f;

        if (hits >= hitsNeeded)
        {
            used = true;
            if (manager != null) manager.TaskDone(taskIndex);
            Debug.Log("watered " + taskIndex);
        }
    }
}
