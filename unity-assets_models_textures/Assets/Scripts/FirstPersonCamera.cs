using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public GameObject Player;
    public float sensitivity = 2f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

       
        Player.transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
