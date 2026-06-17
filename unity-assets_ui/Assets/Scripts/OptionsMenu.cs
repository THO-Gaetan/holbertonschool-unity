using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Back();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        backButton.onClick.AddListener(() => {
            if (string.IsNullOrEmpty(SceneHistory.previousScene))
                SceneManager.LoadScene("MainMenu");
            else
                SceneManager.LoadScene(SceneHistory.previousScene);
        });
    }
}
