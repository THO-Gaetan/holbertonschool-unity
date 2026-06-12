using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject thirdPersonCam;
    public GameObject firstPersonCam;
    public Rigidbody rb;
    private Vector3 movement = new Vector3(0, 0, 0);
    float speed = 1.75f;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (thirdPersonCam.activeSelf)
        {
            movement.x = Input.GetKey(KeyCode.W) ? 1 :
            Input.GetKey(KeyCode.S) ? -1 : 0;

            movement.z = Input.GetKey(KeyCode.A) ? 1 :
            Input.GetKey(KeyCode.D) ? -1 : 0;
        }
        else if (firstPersonCam.activeSelf)
        {
            movement.x = Input.GetKey(KeyCode.D) ? 1 :
            Input.GetKey(KeyCode.A) ? -1 : 0;

            movement.z = Input.GetKey(KeyCode.W) ? 1 :
            Input.GetKey(KeyCode.S) ? -1 : 0;
        }

        UpdateJump();
        UpdateClimb();

        if (Input.GetKey(KeyCode.LeftShift))
            speed = 3.5f;
        else
            speed = 2.0f;
        
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void FixedUpdate()
    {
        Vector3 moveDir = transform.forward * movement.z + transform.right * movement.x;

        rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);
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
            thirdPersonCam.SetActive(false);
            firstPersonCam.SetActive(true);
        }
        if(other.gameObject.CompareTag("Teleporter"))
        {
            transform.position = new Vector3(61f, 21.5f, 28.75f);
            thirdPersonCam.SetActive(true);
            firstPersonCam.SetActive(false);
            StartCoroutine(ActiveBall());
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
            rb.AddForce(Vector3.up * jumpForce * 7f, ForceMode.Impulse);
    }

    IEnumerator ActiveBall()
    {
        FallingBall.SetActive(true);
        yield return new WaitForSeconds(2);
        FallingBall.GetComponent<Rigidbody>().useGravity = true;
    }
}
