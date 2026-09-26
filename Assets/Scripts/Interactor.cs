using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float distance = 3f;

    // this name is how the tasks know what I'm holding, so it has to match their required tool
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
                // I use SendMessage because Interact() is private on the task scripts.
                // DontRequireReceiver stops it complaining about things that can't be used.
                hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void DropCarried()
    {
        if (carriedObj != null) carriedObj.GetComponent<ToolPickup>().SendBack();
        carried = "";
        carriedObj = null;
    }
}
