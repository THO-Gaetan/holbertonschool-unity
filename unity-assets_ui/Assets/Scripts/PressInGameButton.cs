using UnityEngine;

public class PressInGameButton : MonoBehaviour
{
    [SerializeField] private GameObject MoveWall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Button Pressed");
        MoveWall.SetActive(false);
    }
}
