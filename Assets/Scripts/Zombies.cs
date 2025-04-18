using UnityEngine;

public class Zombies : MonoBehaviour
{
    public float health = 100;

    private void Update()
    {
        if (PlayerController.playerDead || Enemy.gameWin)
        {
            this.enabled = false;
        }
        if (MainMenu.playAgain)
        {
            this.enabled = true;
        }
    }
    void RemoveHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
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
}
