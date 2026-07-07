using UnityEngine;
using System.Collections;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public MonoBehaviour timerScript;
    public TMP_Text timerText;
    public TMP_Text FinishTimeText;
    public GameObject winCanvas;
    public GameObject timerCanvas;
    public GameObject EventManager;
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
            Time.timeScale = 0f;
            FinishTimeText.text = timerText.text;
            timerScript.enabled = false;
            timerCanvas.SetActive(false);
            EventManager.SetActive(false);
            winCanvas.SetActive(true);
        }
    }
}
