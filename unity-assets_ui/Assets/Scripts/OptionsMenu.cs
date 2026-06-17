using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button applyButton;
    [SerializeField] private Toggle invertToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        invertToggle.isOn  = PlayerPrefs.GetInt("InvertY", 0) == 1;
        Back();
        Apply();
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

    public void Apply()
    {
        applyButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt("InvertY", invertToggle.isOn ? 1 : 0);
            SceneManager.LoadScene(SceneHistory.previousScene);
        });
    }
}
