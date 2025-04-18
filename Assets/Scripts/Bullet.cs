using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    public float maxRange = 40.0f;
    public static float bulletDamage = 20f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed * -1);
        if (PlayerController.playerDead)
        {
            this.enabled = false;
        }

    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("barrelAk") || other.gameObject.CompareTag("barrelShotgun"))
        {
            Destroy(gameObject);
        }
        
    }
}
