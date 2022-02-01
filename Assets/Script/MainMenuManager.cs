using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelToLoad;
    [SerializeField] private GameObject cr�ditsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    public void Play()
    {
        OptionManager.instance.isInMainMenu = false;
        SceneManager.LoadScene(levelToLoad);
    }
    public void Credits()
    {
        if (cr�ditsPanel.activeInHierarchy)
        {
            cr�ditsPanel.SetActive(false);
        }
        else
        {
            cr�ditsPanel.SetActive(true);
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
