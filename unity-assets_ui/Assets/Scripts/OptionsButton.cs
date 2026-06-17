using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private Button optionsButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Options();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Options()
    {
        optionsButton.onClick.AddListener (( ) => {
            SceneHistory.previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Options");
        });
    }
}
