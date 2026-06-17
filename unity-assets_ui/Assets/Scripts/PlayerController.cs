using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int RandomNumber;
    [SerializeField] private GameObject[] TrapPlatforms;
    [SerializeField] private GameObject stairTrap;
    [SerializeField] private Camera thirdPersonCam;
    [SerializeField] private Camera firstPersonCam;
    public Rigidbody rb;
    private Vector3 movement = new Vector3(0, 0, 0);
    float speed = 1.75f;
    float initialSpeed = 1.75f;
    float jumpForce = 2.5f;
    bool jumpRequested = false;

    public Transform groundCheck;
    public LayerMask WhatIsGround;
    float groundCheckRadius = 0.1f;
 
    public Transform ClimbCheck;
    public LayerMask WhatIsClimbable;
    bool canClimb = false;
    float ClimbSpeed = 1.10f;

    public GameObject FallingBall;
    public bool controlEnabled = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomNumberGenerator();
        for (int i = 0; i < TrapPlatforms.Length; i++)
            TrapPlatforms[i].GetComponent<MeshCollider>().enabled = (i == RandomNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (thirdPersonCam.gameObject.activeSelf)
        {
            movement.x = Input.GetKey(KeyCode.W) ? 1 :
            Input.GetKey(KeyCode.S) ? -1 : 0;

            movement.z = Input.GetKey(KeyCode.A) ? 1 :
            Input.GetKey(KeyCode.D) ? -1 : 0;
        }
        else if (firstPersonCam.gameObject.activeSelf)
        {
            movement.x = Input.GetKey(KeyCode.D) ? 1 :
            Input.GetKey(KeyCode.A) ? -1 : 0;

            movement.z = Input.GetKey(KeyCode.W) ? 1 :
            Input.GetKey(KeyCode.S) ? -1 : 0;
        }

        UpdateJump();
        UpdateClimb();

        if (Input.GetKey(KeyCode.LeftShift))
            speed = initialSpeed * 1.4f;
        else
            speed = initialSpeed;

    }

    void FixedUpdate()
    {
        if (!controlEnabled) return;
        
        Vector3 moveDir = transform.forward * movement.z + transform.right * movement.x;
        moveDir.Normalize();

        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);
        transform.rotation = Quaternion.Euler(0, thirdPersonCam.transform.eulerAngles.y - 90, 0);

        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, WhatIsGround);
    }
    
    bool isClimbing()
    {
        return Physics.CheckSphere(ClimbCheck.position, groundCheckRadius, WhatIsClimbable);
    }
    void UpdateJump()
    {
        if (Input.GetButtonDown("Jump"))
            jumpRequested = true;
    }

    void UpdateClimb()
    {
        if (isClimbing() || canClimb)
        {
            Debug.Log("Climbing");
            rb.useGravity = false;
            if (Input.GetKey(KeyCode.W))
                rb.MovePosition(transform.position + Vector3.up * ClimbSpeed * Time.fixedDeltaTime);
            else if (Input.GetKey(KeyCode.S))
                rb.MovePosition(transform.position + Vector3.down * ClimbSpeed * Time.fixedDeltaTime);
            else
                rb.linearVelocity = Vector3.zero;
        }
        else
            rb.useGravity = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kill"))
            transform.position = new Vector3(0, 20, 0);
        if (other.gameObject.CompareTag("Climb"))
            canClimb = true;
        if (other.gameObject.CompareTag("ChangeCam"))
        {
            thirdPersonCam.gameObject.SetActive(false);
            firstPersonCam.gameObject.SetActive(true);
            firstPersonCam.cullingMask &= ~(1 << LayerMask.NameToLayer("PlayerBody"));
        }
        if(other.gameObject.CompareTag("Teleporter"))
        {
            transform.position = new Vector3(61f, 21.5f, 28.75f);
            thirdPersonCam.gameObject.SetActive(true);
            firstPersonCam.gameObject.SetActive(false);
            firstPersonCam.cullingMask |= (1 << LayerMask.NameToLayer("PlayerBody"));
            StartCoroutine(ActiveBall());
        }
        if (other.gameObject.CompareTag("ChangeCam3D"))
        {
            thirdPersonCam.gameObject.SetActive(true);
            firstPersonCam.gameObject.SetActive(false);
            firstPersonCam.cullingMask |= (1 << LayerMask.NameToLayer("PlayerBody"));
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Climb"))
            canClimb = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trampo"))
            rb.AddForce(Vector3.up * jumpForce * 6f, ForceMode.Impulse);
        if(collision.gameObject.CompareTag("DesactivateTrap"))
            stairTrap.GetComponent<MeshCollider>().enabled = true;
    }

    int RandomNumberGenerator()
    {
        RandomNumber = Random.Range(0, TrapPlatforms.Length);
        return RandomNumber;
    }

    public void StunUntilGrounded()
    {
        controlEnabled = false;
        StartCoroutine(WaitForGround());
    }

    private IEnumerator WaitForGround()
    {
        yield return new WaitForSeconds(0.2f);

        while (!isGrounded())
            yield return null;

        controlEnabled = true;
    }

    private IEnumerator ActiveBall()
    {
        FallingBall.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        FallingBall.GetComponent<Rigidbody>().useGravity = true;
    }
}
