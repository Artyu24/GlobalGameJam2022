using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelToLoad;
    [SerializeField] private GameObject créditsPanel;

    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Options()
    {
        Debug.Log("open option");
    }

    public void Credits()
    {
        if (créditsPanel.activeInHierarchy)
        {
            créditsPanel.SetActive(false);
        }
        else
        {
            créditsPanel.SetActive(true);
        }
    }
}
