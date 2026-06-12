using UnityEngine;
using System.Collections;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public MonoBehaviour timerScript;
    public TMP_Text timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timerScript.enabled = false;
            timerText.color = Color.green;
            timerText.fontSize = 60;
        }
    }
}
