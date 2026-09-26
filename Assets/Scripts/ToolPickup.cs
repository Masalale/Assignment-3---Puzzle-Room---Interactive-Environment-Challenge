using UnityEngine;

public class ToolPickup : MonoBehaviour
{
    public string toolName = "hose";
    public AudioClip pickupSound;

    Vector3 startPos;
    Interactor inter;

    void Start()
    {
        startPos = transform.position;
        inter = FindFirstObjectByType<Interactor>();
    }

    void Interact()
    {
        if (inter == null) return;

        string name = toolName;
        // the decoy tools are all set to "random" so I fall back to the object name
        if (name == "" || name.ToLower() == "random") name = gameObject.name;

        if (inter.carried == name) return;

        if (inter.carried != "") inter.DropCarried();

        inter.carried = name;
        inter.carriedObj = gameObject;

        if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        // hiding the object is how I show that it's being carried
        gameObject.SetActive(false);
    }

    public void SendBack()
    {
        transform.position = startPos;
        gameObject.SetActive(true);
    }
}
