using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    float elapsedTime = 0.0f;
    float seconds = 0.0f;
    float minutes = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimerCount();
    }
    
    void TimerCount()
    {
        elapsedTime += Time.fixedDeltaTime;
        seconds = Mathf.Floor(elapsedTime % 60);
        minutes = Mathf.Floor(elapsedTime / 60);
        timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, Mathf.Floor((elapsedTime % 1) * 100));
    }
}
