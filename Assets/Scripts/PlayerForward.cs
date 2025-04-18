using UnityEngine;
using UnityEngine.UIElements;

public class PlayerForward : MonoBehaviour
{
    public float playerSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        
    }
}
