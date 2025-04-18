using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Pistol : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float cooldown = 0;

    private void Start()
    {
        bulletPrefab.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletPrefab.transform.rotation);
            cooldown = .4f;
        }
        else
        {
            cooldown = cooldown - Time.deltaTime;
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
}
