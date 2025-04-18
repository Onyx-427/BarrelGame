using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public float npcSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.z += -npcSpeed * Time.deltaTime;
        transform.position = newPosition;
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
