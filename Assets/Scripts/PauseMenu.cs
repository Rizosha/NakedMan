using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
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

     void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

     void Paused()
     {
         pauseMenu.SetActive(true);
         Time.timeScale = 0f;
         GameIsPause = true;
     }

     public void Restart()
     {
         SceneManager.LoadScene(1);
     }

    public void QuitGame()
     {
         Application.Quit();
     }
}
