using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float distance = 3f;
    public string carried = "";
    public GameObject carriedObj;
    public bool bucketFilled = false;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (carried != "")
            {
                if (carriedObj != null)
                {
                    ToolPickup tool = carriedObj.GetComponent<ToolPickup>();
                    if (tool != null)
                    {
                        tool.SendBack();
                    }
                    else
                    {
                        carriedObj.transform.position = transform.position + transform.forward * 1f;
                        carriedObj.SetActive(true);
                    }
                }
                carried = "";
                carriedObj = null;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                hit.collider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
