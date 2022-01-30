using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void GamePaused()
    {
        if (GameManager.Instance.isGamePaused)
        {
            GameManager.Instance.isGamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Debug.Log("Pause Off");
        }
        else
        {
            GameManager.Instance.isGamePaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Debug.Log("Pause On");
        }
    }

    public void Resume()
    {
        GamePaused();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
