using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button[] LevelButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Button button in LevelButtons)
        {
            string sceneName = button.name;
            button.onClick.AddListener(() => SceneManager.LoadScene(sceneName));
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
