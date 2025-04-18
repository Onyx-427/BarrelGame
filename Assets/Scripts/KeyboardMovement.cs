using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    public float horizontalInput;
    public float sideBound;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        if (PlayerController.playerDead || Enemy.gameWin)
        {
            this.enabled = false;

        }
        if (MainMenu.playAgain)
        {
            this.enabled = true;
        }

    }
    void playerMovement()
    {
        if (!PlayerController.touchingPlayer)
        {
            horizontalInput = Input.GetAxis("Horizontal"); //move from left to right
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

            if (transform.position.x > sideBound)  //keep player from going offscreen
            {
                transform.position = new Vector3(sideBound, transform.position.y, transform.position.z);
            }
            if (transform.position.x < -sideBound)
            {
                transform.position = new Vector3(-sideBound, transform.position.y, transform.position.z);
            }
        }
        
    }
}
