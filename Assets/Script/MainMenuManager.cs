using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelToLoad;
    [SerializeField] private GameObject créditsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    public void Play()
    {
        OptionManager.instance.isInMainMenu = false;
        SceneManager.LoadScene(levelToLoad);
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

    public void HowToPlay()
    {
        if (howToPlayPanel.activeInHierarchy)
        {
            howToPlayPanel.SetActive(false);
        }
        else
        {
            howToPlayPanel.SetActive(true);
        }
    }
}
