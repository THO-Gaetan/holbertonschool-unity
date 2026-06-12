using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    private float rotationSpeedY = 3f;
    private Vector3 offset;
    private float rotationY;
    private float rotationX;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        offset = Player.transform.position - transform.position;
        rotationY = transform.eulerAngles.y;
        rotationX = transform.eulerAngles.x;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeedY;
            rotationY += mouseX;

            Quaternion cameraRotation = Quaternion.Euler(0f, rotationY, 0f);
            Vector3 rotatedOffset = cameraRotation * offset;

            transform.position = Player.transform.position - rotatedOffset;
            transform.LookAt(Player.transform.position);

        }

        if (!Input.GetMouseButton(1))
        {
            transform.position = Player.transform.position - Quaternion.Euler(0f, rotationY, 0f) * offset;
            transform.LookAt(Player.transform.position);
        }
    }
}