using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// responsible for menu buttons
    /// </summary>
    
    public static bool GameIsPause = false;

    public GameObject pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    //sets the time scale back to normal and return game to normal
     void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    //stops time and pauses the game
     void Paused()
     {
         pauseMenu.SetActive(true);
         Time.timeScale = 0f;
         GameIsPause = true;
     }
    //reloads the scene if you press restart
     public void Restart()
     {
         SceneManager.LoadScene(1);
     }
    //quits game
    public void QuitGame()
     {
         Application.Quit();
     }
}
