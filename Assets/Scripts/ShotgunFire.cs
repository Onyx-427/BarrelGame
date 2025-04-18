using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ShotgunFire : MonoBehaviour
{
    public GameObject bulletPrefab;       // Drag your bullet prefab here
    public Transform barrelEnd;           // The "muzzle" of your gun
    public int pelletCount = 5;           // How many bullets per shot
    public float spreadAngle = 15f;       // Spread cone angle in degrees
    public float bulletForce = 20f;       // How fast bullets go
    public float cooldown;

    void Update()
    {
        if (cooldown <= 0)
        {
            FireShotgun();
            cooldown = .6f;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
        if (SpawnManager.gunReset)
        {
            Destroy(gameObject);
            SpawnManager.gunReset = false;

        }
        if (PlayerController.playerDead || Enemy.gameWin)
        {
            this.enabled = false;
        }
        if (MainMenu.playAgain)
        {
            this.enabled = true;
        }
    }

    void FireShotgun()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // Random spread within a cone
            float angleY = Random.Range(-spreadAngle, spreadAngle);
            float angleX = Random.Range(-spreadAngle, spreadAngle);
            Quaternion spreadRotation = Quaternion.Euler(angleX, angleY, 0);
            Vector3 shootDirection = spreadRotation * barrelEnd.forward;

            GameObject bullet = Instantiate(bulletPrefab, barrelEnd.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * bulletForce;
            }

        }
    }
}