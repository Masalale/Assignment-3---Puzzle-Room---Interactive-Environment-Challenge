using UnityEngine;

// Raycasts from the camera to interact, and tracks the carried tool.
public class Interactor : MonoBehaviour
{
    public float distance = 3f;

    // Tool name the pot scripts check against.
    public string carried = "";
    public GameObject carriedObj;
    public bool bucketFilled = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (carried != "") DropCarried();
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                // Interact() is private on the task scripts, so it is called by name.
                hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    // Shared by the Q key and by the light pots when the lamp runs out.
    public void DropCarried()
    {
        if (carriedObj != null) carriedObj.GetComponent<ToolPickup>().SendBack();
        carried = "";
        carriedObj = null;
    }
}
