using UnityEngine;

public class Cameralook : MonoBehaviour
{
    public float mouseSensitifity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitifity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitifity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
