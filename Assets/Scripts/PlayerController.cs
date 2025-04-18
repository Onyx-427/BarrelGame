using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{


    public GameObject playerPrefab;
    static public bool touchingWall = false;
    public static bool touchingPlayer = false;
    public GameObject pistolPrefab;
    public GameObject akPrefab;
    public GameObject shotgunPrefab;
    public static int cloneCount = 0;
    private Animator animator;
    public static bool hasAk;
    public static bool hasShotgun;
    public static bool cloning = false;
    public static int touchingWallCount;
    private float deathCounter = 0;
    private int playerCount = 1;
    public static bool playerDead = false;
    public List<GameObject> clones = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("notPistol", false);
        clones.Add(playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] akCount = GameObject.FindGameObjectsWithTag("ak");
        GameObject[] shotgunCount = GameObject.FindGameObjectsWithTag("shotgun");
        GameObject[] pistolCount = GameObject.FindGameObjectsWithTag("pistol");
        if (Enemy.ak || Enemy.shotgun)
        {
            animator.SetBool("notPistol", true);
        }
        
        deathCounter += Time.deltaTime;
        if (clones.Count == 0)
        {
            animator.StopPlayback();
            animator.SetBool("isDead", true);
            playerDead = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            touchingWall = true;
            touchingWallCount++;
            if (touchingWallCount < 3)
            {
                WallBehavior wallStats = other.GetComponent<WallBehavior>();
                playerCount = wallStats.randomAdd;
                createClones(wallStats.randomAdd);
                Destroy(other.gameObject); 

            }

        }
    }
    void createClones(int playersNum)
    {
        if (playersNum > 0)
        {
            for (int i = 0; i < playersNum; i++)
            {
                if (i < 4)
                {
                    GameObject playerClone = Instantiate(playerPrefab, new Vector3(i, playerPrefab.transform.position.y, playerPrefab.transform.position.z), playerPrefab.transform.rotation);
                    clones.Add(playerClone);
                    if (WallBehavior.cloneAk)
                    {
                        GameObject akClone = Instantiate(akPrefab, new Vector3(i + .224f, akPrefab.transform.position.y, playerPrefab.transform.position.z), akPrefab.transform.rotation);
                        playerCount = 0;
                    }
                    if (WallBehavior.cloneShotgun)
                    {
                        GameObject shotgunClone = Instantiate(shotgunPrefab, new Vector3(i + .224f, shotgunPrefab.transform.position.y, playerPrefab.transform.position.z - .5f), shotgunPrefab.transform.rotation);
                        playerCount = 0;
                    }
                    if (WallBehavior.clonePistol)
                    {
                        GameObject pistolClone = Instantiate(pistolPrefab, new Vector3(i - .2f, pistolPrefab.transform.position.y, playerPrefab.transform.position.z), pistolPrefab.transform.rotation);
                        playerCount = 0;
                    }

                }
                else
                {
                    GameObject playerClone = Instantiate(playerPrefab, new Vector3(i, playerPrefab.transform.position.y, playerPrefab.transform.position.z - 2), playerPrefab.transform.rotation);
                    if (WallBehavior.cloneAk)
                    {
                        GameObject akClone = Instantiate(akPrefab, new Vector3(i + .224f, akPrefab.transform.position.y, playerPrefab.transform.position.z - 2), akPrefab.transform.rotation);
                        playerCount = 0;
                    }
                    if (WallBehavior.cloneShotgun)
                    {
                        GameObject shotgunClone = Instantiate(shotgunPrefab, new Vector3(i + .224f, shotgunPrefab.transform.position.y, playerPrefab.transform.position.z - 2.5f), shotgunPrefab.transform.rotation);
                        playerCount = 0;
                    }
                    if (WallBehavior.clonePistol)
                    {
                        GameObject pistolClone = Instantiate(pistolPrefab, new Vector3(i - .2f, pistolPrefab.transform.position.y, playerPrefab.transform.position.z - 2), pistolPrefab.transform.rotation);
                        playerCount = 0;
                    }
                }
            }
        }
        if (playersNum <= 0)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            int killCount = 0;
            foreach (GameObject clone in players)
            {
                if (killCount <= playerCount)
                {
                    break;
                }
                animator = clone.GetComponent<Animator>();
                animator.SetBool("isDead", true);
                
                if (clones.Count > 2)
                {
                    GameObject clonesToDestroy = clones[0];
                    clones.RemoveAt(0);
                    Destroy(clonesToDestroy);
                }
                if (clones.Count == 1)
                {
                    GameObject clonesToDestroy = clones[0];
                    clones.RemoveAt(0);
                    animator.StopPlayback();
                    animator.SetBool("isDead", true);
                    playerDead = true;
                }
                killCount--;
            }
        }
    }

}
