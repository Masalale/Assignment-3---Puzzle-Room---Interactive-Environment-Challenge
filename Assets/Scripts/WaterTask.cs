using UnityEngine;

public class WaterTask : MonoBehaviour
{
    public PuzzleManager manager;
    public int taskIndex = 0;
    public int hitsNeeded = 3;
    public Color wetColor = new Color(0.3f, 0.5f, 0.25f);
    public Renderer leaf;

    int hits = 0;
    bool used = false;

    void Interact()
    {
        if (used) return;

        hits++;
        transform.localScale = transform.localScale * 1.05f;

        if (leaf != null) leaf.material.color = wetColor;

        if (hits >= hitsNeeded)
        {
            used = true;
            if (manager != null) manager.TaskDone(taskIndex);
            Debug.Log("watered " + taskIndex);
        }
    }
}
