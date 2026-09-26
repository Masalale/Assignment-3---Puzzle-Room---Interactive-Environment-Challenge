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
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public AudioClip turnSound;
    public AudioClip doneSound;
    public AudioClip denySound;
    public string requiredTool = "lamp";

    int turns = 0;
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
        if (inter == null || inter.carried != requiredTool)
        {
            AudioSource denyAudio0 = GetComponent<AudioSource>();
            if (denyAudio0 != null && denySound != null) denyAudio0.PlayOneShot(denySound);
            return;
        }

        if (needsOthersFirst && manager != null && manager.Count() < needCount)
        {
            AudioSource denyAudio = GetComponent<AudioSource>();
            if (denyAudio != null && denySound != null) denyAudio.PlayOneShot(denySound);
            return;
        }

        turns++;
        transform.Rotate(0, 90, 0);
        if (turns == 1 && turnsNeeded > 1) ShowStage(2);
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
            ShowStage(3);
            if (inter != null)
            {
                ToolPickup tp = null;
                if (inter.carriedObj != null) tp = inter.carriedObj.GetComponent<ToolPickup>();
                inter.carried = "";
                inter.carriedObj = null;
                if (tp != null) tp.SendBack();
            }
            AudioSource doneAudio = GetComponent<AudioSource>();
            if (doneAudio != null && doneSound != null) doneAudio.PlayOneShot(doneSound);
        }
    }
}
