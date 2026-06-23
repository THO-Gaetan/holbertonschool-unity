using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainMenu();
        Next();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        });
    }

    public void Next()
    {
        nextButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }
}
