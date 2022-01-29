using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool isGamePaused;

    public void GamePaused()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            isGamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void Resume()
    {
        GamePaused();
    }

    public void OptionMenu()
    {

    }

    public void MainMenu()
    {
        
    }
}
