using UnityEngine;

public class PuzzleTask : MonoBehaviour
{
    public PuzzleManager manager;
    public int taskIndex = 0;
    public bool used = false;

    void Interact()
    {
        if (used) return;
        used = true;

        if (manager != null)
        {
            manager.TaskDone(taskIndex);
        }

        // simple feedback - hide a bit or change color later
        Debug.Log("task done " + taskIndex);
    }
}
