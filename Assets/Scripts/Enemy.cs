using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float rotationSpeed;
    public float health = 100;
    public static bool ak = false;
    public static bool shotgun = false;
    private float spawnAKX;
    [SerializeField] public TextMeshProUGUI uiText;
    private Animator animator;
    public static bool gameWin = false;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
        UpdateUIText();
        if (health <= 0)
        {
            if (gameObject.tag == "zombie")
            {
                animator = GetComponent<Animator>();
                animator.SetBool("isDead", true);

            }
            Destroy(gameObject);
            if (gameObject.tag == "barrelAk")
            { 
                ak = true;
            }
            if (gameObject.tag == "barrelShotgun")
            {
                shotgun = true; 
            }
            if (gameObject.tag == "boss")
            {
                gameWin = true;
            }
            
        }
        
    }
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            RemoveHealth(Bullet.bulletDamage);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        
        
    }
    public void RemoveHealth(float amount)
    {
        health -= amount;
    }
    void UpdateUIText()
    {
        if (uiText != null)
        {
            uiText.text = health.ToString();
        }

    }
   
}
