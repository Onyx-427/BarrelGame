using System.Threading;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] barrelPrefab;
    public GameObject wallPrefab;
    public GameObject playerPrefab;
    public Transform player;
    public GameObject enemyHealth;
    public GameObject[] zombiePrefabs;
    public GameObject[] guns;
    public GameObject akOnBarrel;
    public GameObject shotgunOnBarrel;
    private int barrelIndex;
    private int zombieIndex;
    public float spawnEnemyZ;
    public float spawnEnemyY = 0.9f;
    public float spawnDelayEnemy = 2f;
    public float spawnIntervalEnemy = 1f;
    public float spawnMinX = -3.5f;
    public float spawnMaxX = 3.5f;
    public float spawnWallX = 1f;
    public float spawnWallZ;
    public float spawnDelayWall;
    public float spawnIntervalWall;
    public float spawnPlayerX;
    public float spawnPlayerZ;
    public float spawnPlayerY;
    public float spawnDelayZombie;
    public float spawnIntervalZombie;
    public float spawnZombieY;
    private float spawnPistolX;
    private float spawnPistolY = 1.4f;
    private float spawnPistolZ = -96.456f;
    public GameObject bossPrefab;
    public static bool playerHasAk = false;
    public static bool playerHasShotgun = false;
    public static bool gunReset = false;
    private float wallSpawnDelay = 0;
    void Start()
    {
        InvokeRepeating("SpawnRandomBarrel", spawnDelayEnemy, spawnIntervalEnemy);
        InvokeRepeating("SpawnRandomZombie", spawnDelayZombie, spawnIntervalZombie);
        SpawnPistol();
        SpawnWall();
        spawnEnemyZ = Random.Range(10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.ak)
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
            gunReset = true;
            foreach (GameObject player in  playerObjects)
            {
                GameObject akClone = Instantiate(guns[1], new Vector3(player.transform.position.x + .224f, guns[1].transform.position.y, guns[1].transform.position.z), guns[1].transform.rotation);
            }
            Enemy.ak = false;
            playerHasAk = true;
        }
        if (Enemy.shotgun)
        {
            gunReset = true;
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in playerObjects)
            {
                GameObject shotgunClone = Instantiate(guns[2], new Vector3(player.transform.position.x + .224f, guns[2].transform.position.y, guns[2].transform.position.z), guns[2].transform.rotation);
            }
            Enemy.shotgun = false;
            playerHasShotgun = true;
        }
        if (PlayerController.touchingWallCount == 2)
        {
            CancelInvoke("SpawnRandomBarrel");
            CancelInvoke("SpawnWall");
            CancelInvoke("SpawnRandomZombie");
            SpawnBoss();
            PlayerController.touchingWallCount = 3;
        }
        if (PlayerController.touchingWall && PlayerController.touchingWallCount < 3)
        {
            wallSpawnDelay += Time.deltaTime;
            if (wallSpawnDelay > 10)
            {
                SpawnWall();
                wallSpawnDelay = 0;
            }
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

    void SpawnRandomBarrel()
    {
        //Randomly gen enemy spawn position (x)
        float randomX = Random.Range(spawnMinX, spawnMaxX);
        Vector3 spawnPositionEnemy = new Vector3(randomX, spawnEnemyY, player.position.z + spawnEnemyZ);
        Vector3 spawnPositionEnemyHealth = new Vector3(randomX - 1f, spawnEnemyY + .1f, player.position.z + spawnEnemyZ - 1f);

        barrelIndex = Random.Range(0, barrelPrefab.Length);

        Quaternion spawnRotationEnemy = barrelPrefab[barrelIndex].transform.rotation;
        Quaternion spawnRotationEnemyHealth = enemyHealth.transform.rotation;
        Instantiate(barrelPrefab[barrelIndex], spawnPositionEnemy, spawnRotationEnemy);
        Instantiate(enemyHealth.transform, spawnPositionEnemyHealth, spawnRotationEnemyHealth);
        Quaternion spawnRotationGun = Quaternion.Euler(0, 90, 0);
        Vector3 spawnPositionShotgun = new Vector3(randomX - 1.1f, spawnEnemyY + 1.2f, player.position.z + spawnEnemyZ);
        Vector3 spawnPositionAk = new Vector3(randomX - 1.1f, spawnEnemyY + 1.2f, player.position.z + spawnEnemyZ);
        if (barrelIndex == 1)
        {
            Instantiate(akOnBarrel, spawnPositionAk, spawnRotationGun);    
        }
        if (barrelIndex == 2)
        {
            Instantiate(shotgunOnBarrel, spawnPositionShotgun, spawnRotationGun);
        }
    }

    void SpawnWall()
    {
        Vector3 spawnPositionL = new Vector3(-spawnWallX, 1, player.position.z + spawnWallZ);
        Vector3 spawnPositionR = new Vector3(spawnWallX, 1, player.position.z + spawnWallZ);

        Quaternion spawnRotationWall = wallPrefab.transform.rotation;
        Instantiate(wallPrefab.transform, spawnPositionL, spawnRotationWall);
        Instantiate(wallPrefab.transform, spawnPositionR, spawnRotationWall);
    }

    void SpawnPlayer()
    {
        Vector3 spawnPositionPlayer = new Vector3(spawnPlayerX + 5, spawnPlayerY, spawnPlayerZ);
        Quaternion spawnRotationPlayer = playerPrefab.transform.rotation;
    }

    void SpawnRandomZombie()
    {
        float randomX = Random.Range(spawnMinX, spawnMaxX);
        Vector3 spawnPositionZombie = new Vector3(randomX, spawnZombieY, player.position.z + spawnEnemyZ);

        zombieIndex = Random.Range(0, zombiePrefabs.Length);

        Quaternion spawnRotationZombie = zombiePrefabs[zombieIndex].transform.rotation;
        Instantiate(zombiePrefabs[zombieIndex], spawnPositionZombie, spawnRotationZombie);
    }

    void SpawnPistol()
    {
        spawnPistolX = playerPrefab.transform.position.x;
        spawnPistolX -= .01f;
        Vector3 spawnPosition = new Vector3(spawnPistolX, spawnPistolY, spawnPistolZ);
        Quaternion spawnRotation = guns[0].transform.rotation;
        Instantiate(guns[0], spawnPosition, spawnRotation);
    }

    void SpawnBoss()
    {
        Vector3 spawnPosition = new Vector3(0, .5f, playerPrefab.transform.position.z + 10);
        Quaternion spawnRotation = bossPrefab.transform.rotation;
        Instantiate(bossPrefab.transform, spawnPosition, spawnRotation);
    }
}
