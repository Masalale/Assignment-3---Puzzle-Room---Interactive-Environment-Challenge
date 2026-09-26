using UnityEngine;

// Light pot logic. Requires a tool, grows the plant, then consumes the tool.
public class LightAlignTask : MonoBehaviour
{
    public PuzzleManager manager;
    public int taskIndex = 2;
    public int turnsNeeded = 2;
    public bool needsOthersFirst = false;
    public int needCount = 4;
    public Light growLight;
    public Color doneColor = Color.green;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public AudioClip turnSound;
    public AudioClip doneSound;
    public AudioClip denySound;
    public string requiredTool = "lamp";

    Interactor inter;
    AudioSource sfx;
    int turns = 0;
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

        if (inter == null || inter.carried != requiredTool)
        {
            if (sfx != null && denySound != null) sfx.PlayOneShot(denySound);
            return;
        }

        if (needsOthersFirst && manager != null && manager.Count() < needCount)
        {
            if (sfx != null && denySound != null) sfx.PlayOneShot(denySound);
            return;
        }

        turns++;
        transform.Rotate(0, 90, 0);
        if (turns == 1 && turnsNeeded > 1) ShowStage(2);
        if (sfx != null && turnSound != null) sfx.PlayOneShot(turnSound);

        if (turns >= turnsNeeded)
        {
            used = true;
            if (growLight != null)
            {
                growLight.color = doneColor;
                growLight.intensity = 2f;
            }
            if (manager != null) manager.TaskDone(taskIndex);
            ShowStage(3);
            inter.DropCarried();
            if (sfx != null && doneSound != null) sfx.PlayOneShot(doneSound);
        }
    }
}
