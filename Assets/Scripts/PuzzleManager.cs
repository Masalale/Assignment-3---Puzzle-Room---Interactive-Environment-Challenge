using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public Text progressText;
    public DoorController door;

    bool[] done = new bool[5];

    void Start()
    {
        UpdateUI();
    }

    public void TaskDone(int index)
    {
        if (index < 0 || index > 4) return;
        if (done[index]) return;

        done[index] = true;
        UpdateUI();

        if (IsAllDone())
        {
            if (door != null) door.Open();
        }
    }

    public int Count()
    {
        int c = 0;
        for (int i = 0; i < 5; i++) if (done[i]) c++;
        return c;
    }

    public bool IsAllDone()
    {
        return Count() >= 5;
    }

    void UpdateUI()
    {
        if (progressText != null)
        {
            progressText.text = "Puzzle Progress: " + Count() + " / 5";
        }
    }
}
