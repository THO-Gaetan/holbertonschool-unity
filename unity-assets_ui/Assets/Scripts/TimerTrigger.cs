using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public MonoBehaviour timerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            timerScript.enabled = true;
            Debug.Log("Timer Started");
    }
}
