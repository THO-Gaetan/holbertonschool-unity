using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Exit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        exitButton.onClick.AddListener(() => { Application.Quit(); Debug.Log("Exited"); });
    }
}
