using UnityEngine;

public class ToolPickup : MonoBehaviour
{
    public string toolName = "hose";
    public AudioClip pickupSound;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Interact()
    {
        Interactor inter = FindFirstObjectByType<Interactor>();
        if (inter == null) return;

        string actualToolName = toolName;
        if (actualToolName == "" || actualToolName.ToLower() == "random")
        {
            actualToolName = gameObject.name;
        }

        if (inter.carried == actualToolName) return;

        if (inter.carried != "" && inter.carriedObj != null)
        {
            ToolPickup oldTool = inter.carriedObj.GetComponent<ToolPickup>();
            if (oldTool != null)
            {
                oldTool.SendBack();
            }
            else
            {
                inter.carriedObj.transform.position = inter.transform.position + inter.transform.forward * 1.2f;
                inter.carriedObj.transform.rotation = inter.transform.rotation;
                inter.carriedObj.SetActive(true);
            }
            inter.carried = "";
            inter.carriedObj = null;
        }

        inter.carried = actualToolName;
        inter.carriedObj = gameObject;

        PlayPickupSound();

        gameObject.SetActive(false);
    }

    void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            return;
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source != null && !source.loop && source.clip != null)
            {
                AudioSource.PlayClipAtPoint(source.clip, transform.position);
                return;
            }
        }

        foreach (AudioSource source in sources)
        {
            if (source != null && source.clip != null)
            {
                AudioSource.PlayClipAtPoint(source.clip, transform.position);
                return;
            }
        }
    }

    public void SendBack()
    {
        transform.position = startPos;
        gameObject.SetActive(true);
    }
}
