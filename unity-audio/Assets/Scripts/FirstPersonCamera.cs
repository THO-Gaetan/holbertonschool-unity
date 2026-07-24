using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public GameObject Player;
    public float sensitivity = 2f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    [SerializeField] public bool isInverted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse X") * sensitivity;
        float mouseX = Input.GetAxis("Mouse Y") * sensitivity;

        rotationY += mouseY;
        if (isInverted)
            rotationX += mouseX;
        else
            rotationX -= mouseX;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

       
        Player.transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

}
