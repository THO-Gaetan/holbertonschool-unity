using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed = 8.0f;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public GameObject winloseCanvas;
    public Image winloseImage;
    public Text winloseText;
    private int score = 0;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            SetLoseCanvas();
            enabled = false;
            StartCoroutine(ReloadScene(3));
        }
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("menu");
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.S))
            rb.MovePosition(rb.position - transform.forward * speed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.A))
            rb.MovePosition(rb.position - transform.right * speed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.D))
            rb.MovePosition(rb.position + transform.right * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetScoreText();
            transform.localScale = new Vector3(1 + score * 0.2f, 1 + score * 0.2f, 1 + score * 0.2f);
            if (speed <= 3.5f)
                speed = speed - 0.1f + (score * 0.1f) * 0.010f;
            else
                speed = speed * (1 - (score * 0.5f) * 0.022f);
            
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            health -= 1;
            SetHealthText();
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            if (score >= 3)
            {
                Debug.Log("You win!");
                SetWinCanvas();
                enabled = false;
                StartCoroutine(ReloadScene(5));
            }
            else
            {
                Debug.Log("You need at least 3 points to win!");
            }
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score : " + score;
        Debug.Log("score");
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health;
        Debug.Log("health");
    }

    void SetWinCanvas()
    {
        winloseCanvas.SetActive(true);
        winloseImage.color = new Color(0f, 1f, 0f);
        winloseText.text = "You Win!";
        winloseText.color = new Color(0f, 0f, 0f);
    }

    void SetLoseCanvas()
    {
        winloseCanvas.SetActive(true);
        winloseImage.color = new Color(1f, 0f, 0f);
        winloseText.text = "Game Over!";
        winloseText.color = new Color(1f, 1f, 1f);
    }
    
    IEnumerator ReloadScene(int seconds)
    {
        Debug.Log("Revive after " + seconds + " seconds");
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
