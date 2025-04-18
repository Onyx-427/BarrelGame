using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private bool isCounting = false;
    private float counter = 0;
    public static bool playAgain = false;
    public void Update()
    {
        if (PlayerController.playerDead)
        {
            isCounting = true; 
        }
        if (isCounting)
        {
            counter += Time.deltaTime;
        }
        if (counter > 3)
        {
            SceneManager.LoadSceneAsync(3);
        }
        if (Enemy.gameWin)
        {
            SceneManager.LoadSceneAsync(4);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void HowToPlay()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Back()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void TryAgain()
    {
        SceneManager.LoadSceneAsync(1);
        playAgain = true;
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
