using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    private float rotationSpeed = 3f;
    private Vector3 offset;
    private float distance;
    private float rotationY;
    private float rotationX;

    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 70f;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        offset = Player.transform.position - transform.position;
        distance = offset.magnitude;
        rotationY = 90f;
        rotationX = 20f;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            rotationY += Input.GetAxis("Mouse X") * rotationSpeed;
            rotationX -= Input.GetAxis("Mouse Y") * rotationSpeed;
            rotationX = Mathf.Clamp(rotationX, minPitch, maxPitch);
        }

        Quaternion cameraRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        Vector3 direction = cameraRotation * Vector3.back;

        transform.position = Player.transform.position + direction * distance;
        transform.LookAt(Player.transform.position);
    }
}