using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool opened = false;

    public void Open()
    {
        if (opened) return;
        opened = true;

        // slide door up so player can walk out
        transform.position = transform.position + new Vector3(0, 2.2f, 0);
        Debug.Log("door opened");
    }
}
