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

    int turns = 0;
    bool used = false;

    void Interact()
    {
        if (used) return;

        if (needsOthersFirst && manager != null && manager.Count() < needCount)
        {
            Debug.Log("not yet - finish others first");
            return;
        }

        turns++;
        transform.Rotate(0, 90, 0);

        if (turns >= turnsNeeded)
        {
            used = true;
            if (growLight != null)
            {
                growLight.color = doneColor;
                growLight.intensity = 2f;
            }
            if (manager != null) manager.TaskDone(taskIndex);
            Debug.Log("aligned " + taskIndex);
        }
    }
}
