using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (float time = 0; time < Time.deltaTime * speed; time += Time.deltaTime)
        {
            transform.Rotate(new Vector3(45, 0, 0) * Time.deltaTime);
        }
    }
}
