using UnityEngine;

// Lets a tool be picked up and returned to its start position.
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
        // Decoy tools are set to "random" and fall back to the object name.
        if (name == "" || name.ToLower() == "random") name = gameObject.name;

        if (inter.carried == name) return;

        if (inter.carried != "") inter.DropCarried();

        inter.carried = name;
        inter.carriedObj = gameObject;

        if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        // Hidden while the tool is carried.
        gameObject.SetActive(false);
    }

    public void SendBack()
    {
        transform.position = startPos;
        gameObject.SetActive(true);
    }
}
