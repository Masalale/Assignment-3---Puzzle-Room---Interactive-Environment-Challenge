using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float distance = 3f;
 
    void Update()
    {
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
