using UnityEngine;

public class PlayerHasGun : MonoBehaviour
{
    public static bool playerHasAk = false;
    public static bool playerHasShotgun = false;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.hasAk)
        {
            playerHasAk = true;
        }
        if (PlayerController.hasShotgun)
        {
            playerHasShotgun = true;
        }

    }
}
