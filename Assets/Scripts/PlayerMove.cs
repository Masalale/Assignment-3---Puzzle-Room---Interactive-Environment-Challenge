using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 4f;
    public float mouseSpeed = 2f;

    CharacterController controller;
    Camera playerCam;
    float upDown = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * mouseSpeed;
        float my = Input.GetAxis("Mouse Y") * mouseSpeed;

        transform.Rotate(0, mx, 0);

        upDown -= my;
        upDown = Mathf.Clamp(upDown, -80, 80);
        playerCam.transform.localRotation = Quaternion.Euler(upDown, 0, 0);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        move = move * speed;
        move.y = -2f;

        controller.Move(move * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
