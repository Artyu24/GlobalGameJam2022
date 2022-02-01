using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuccesManager : MonoBehaviour
{
    [SerializeField] private GameObject succesPanel;

    [Header("High Score")]
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject highScoreProgressionBar;
    private int[] currentHighScoreSucces = {0,25,100,250,500,1000,1750,2500,3000,4000,5500,7000,10000};

    [Header("Total Score")]
    [SerializeField] private TMP_Text TotalScoreText;
    [SerializeField] private GameObject TotalScoreProgressionBar;
    private int[] TotalScoreSucces = {0,500,1000,2500,5000,10000,20000,40000,50000,100000, 150000,200000,300000,500000,700000,1500000,2000000};

    [Header("Number Of Game Played")]
    [SerializeField] private TMP_Text NumberOfGamePlayedText;
    [SerializeField] private GameObject NumberOfGamePlayedProgressionBar;
    private int[] NumberOfGamePlayedSucces = {0,5,25,100,200,500,1000,1750,2000,3000,4000,5000,10000};

    [Header("Number Of Swipe")]
    [SerializeField] private TMP_Text NumberOfSwipeText;
    [SerializeField] private GameObject NumberOfSwipeProgressionBar;
    private int[] NumberOfSwipeSucces = {0,5000,10000,20000,40000,50000,100000,150000,200000,300000,500000,700000,1500000,2000000};

    public void OpenSuccesPanel()
    {
        if (succesPanel.activeInHierarchy)
        {
            succesPanel.SetActive(false);
        }
        else
        {
            succesPanel.SetActive(true);

            //MAJ Fonction
            MajSuccesPanel();
        }
    }

    private void MajSuccesPanel()
    {
        int i = 0;
        #region HighScore
        while (PlayerPrefs.GetInt("High Score") >= currentHighScoreSucces[i])
        {
            highScoreText.text = "High Score : " + PlayerPrefs.GetInt("High Score") + " / " + currentHighScoreSucces[i+1];

            float _scale = PlayerPrefs.GetInt("High Score") / (float)currentHighScoreSucces[i + 1];
            highScoreProgressionBar.transform.localScale = new Vector3(_scale, 1, 1);

            i++;
        }
        #endregion

        i = 0;
        #region TotalScore
        while (PlayerPrefs.GetInt("Total Score") >= TotalScoreSucces[i])
        {
            TotalScoreText.text = "Total Score : " + PlayerPrefs.GetInt("Total Score") + " / " + TotalScoreSucces[i + 1];

            float _scale = PlayerPrefs.GetInt("Total Score") / (float)TotalScoreSucces[i + 1];
            TotalScoreProgressionBar.transform.localScale = new Vector3(_scale, 1, 1);

            i++;
        }
        #endregion

        i = 0;
        #region NumberOfGamePlayed
        while (PlayerPrefs.GetInt("Number Of Game Played") >= NumberOfGamePlayedSucces[i])
        {
            NumberOfGamePlayedText.text = "Number Of Game Played : " + PlayerPrefs.GetInt("Number Of Game Played") + " / " + NumberOfGamePlayedSucces[i + 1];

            float _scale = PlayerPrefs.GetInt("Number Of Game Played") / (float)NumberOfGamePlayedSucces[i + 1];
            NumberOfGamePlayedProgressionBar.transform.localScale = new Vector3(_scale, 1, 1);

            i++;
        }
        #endregion

        i = 0;
        #region NumberOfSwipe
        while (PlayerPrefs.GetInt("Number Of Swipe") >= NumberOfSwipeSucces[i])
        {
            NumberOfSwipeText.text = "Number Of Swipe : " + PlayerPrefs.GetInt("Number Of Swipe") + " / " + NumberOfSwipeSucces[i + 1];

            float _scale = PlayerPrefs.GetInt("Number Of Swipe") / (float)NumberOfSwipeSucces[i + 1];
            NumberOfSwipeProgressionBar.transform.localScale = new Vector3(_scale, 1, 1);

            i++;
        }
        #endregion
    }
}
