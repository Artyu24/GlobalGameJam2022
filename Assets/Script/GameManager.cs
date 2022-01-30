using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get => _instance;
    }

    [Header("Vitesse des cubes")] [SerializeField]
    private float speed;

    public float GetSpeed
    {
        get => speed;
    }

    [Header("Acc�l�ration")] private float accel;
    [SerializeField] private float timeBetweenAccel;

    [Header("R�duction du temps � chaque accel (<0.1f)")] [SerializeField]
    private float timeMultiplierDecrease;

    private float timeMultiplier;

    public float GetTimeMultiplier
    {
        get { return timeMultiplier; }
    }

    [Header("Spawn & Cubes")] [SerializeField]
    private GameObject leftSpawn, rightSpawn;

    [SerializeField] private GameObject blackCube, whiteCube;

    [Header("Tableau des paternes")] [SerializeField]
    private PaternCube[] tabPatern;

    [Header("Score")] private int score;
    private int scoreMultiplier;
    [SerializeField] private TMP_Text scoreText;

    [Header("Material")] [SerializeField] private ColorPair[] colorPairs;
    [SerializeField] private float timeBetweenColorChange;
    [SerializeField] private float colorTransitionDuration;
    private int previousColor;
    private bool isColorSwitching;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private GameObject player;
    [SerializeField] private SpeedEffectManager speedEffectManager;
    private float accelSpeedEffect;

    [Header("Defeat")] private string blackTag = "Black";
    private string whiteTag = "White";
    private string actualTag;

    public string getActualTag
    {
        get { return actualTag; }
    }

    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private Text defeatScoreText;
    [SerializeField] private Text defeatHighScoreText;
    private bool _isPlayerAlive = true;

    public bool isGamePaused = false;

    public bool isPlayerAlive
    {
        get { return _isPlayerAlive; }
        set { _isPlayerAlive = value; }
    }

     private bool isPaternPlay;

    public bool GetIsPaternPlay
    {
        get { return isPaternPlay; }
        set { isPaternPlay = value; }
    }

    private bool isWhiteSide;
    private bool isAccelFinish;
    private bool isScoreIncrease;

    private void Awake()
    {
        _instance = this;
        Time.timeScale = 1;

        scoreMultiplier = 1;
        scoreText.text = score.ToString();

        timeMultiplier = 1;
        accel = speed * 0.2f;

        accelSpeedEffect = 0;
        speedEffectManager.UpdateParticles(accelSpeedEffect);

        player.GetComponent<SpriteRenderer>().material = blackMaterial;
        actualTag = whiteTag;

        previousColor = UnityEngine.Random.Range(0, colorPairs.Length);
        whiteMaterial.SetColor("_GlowColor", colorPairs[previousColor].whiteColor);
        blackMaterial.SetColor("_GlowColor", colorPairs[previousColor].blackColor);

        // Kiss xoxo
        // PaternCube[] paterns = Resources.LoadAll<PaternCube>("Patterns"); Load in the "Resources" folder
        // GameObject.FindObjectsOfType<GameManager>(true); Cool but look for the result in case
        // Resources.FindObjectsOfTypeAll<GameManager>().Where(a => a.gameObject.scene.isLoaded); Find everything in scene and in project
    }

    private void Update()
    {
        if (isPlayerAlive)
        {
            if (!isAccelFinish && speed < accel * 15)
                StartCoroutine(AccelGame());


            if (!isScoreIncrease)
                StartCoroutine(ScoreIncrease());

            if (!isPaternPlay)
                PlayRandomPatern();

            if (!isColorSwitching)
                StartCoroutine(ColorSwitch());
        }
        else
        {
            defeatPanel.SetActive(true);
            CheckIfHighScore();
            defeatHighScoreText.text = "High Score : " + PlayerPrefs.GetInt("High Score").ToString();
            defeatScoreText.text = "Score : " + score.ToString();
        }
    }

    private void PlayRandomPatern()
    {
        int random = Random.Range(0, tabPatern.Length);
        tabPatern[random].ActivateCoroutine();
    }

    public void SwitchLine()
    {
        isWhiteSide = !isWhiteSide;

        if (isWhiteSide)
        {
            player.GetComponent<SpriteRenderer>().sharedMaterial = whiteMaterial;
            actualTag = blackTag;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sharedMaterial = blackMaterial;
            actualTag = whiteTag;
        }
    }

    public void SpawnCube(string side, GameObject whichCube)
    {
        GameObject spawnObjet = null;

        if (side == "right")
            spawnObjet = rightSpawn;
        else if (side == "left")
            spawnObjet = leftSpawn;

        if (whichCube != null)
        {
            if (whichCube.CompareTag("White"))
                SpawnWhiteCube(spawnObjet);
            else
                SpawnBlackCube(spawnObjet);
        }
    }

    private void SpawnWhiteCube(GameObject spawnPosition)
    {
        GameObject _whiteCube = Instantiate(whiteCube, spawnPosition.transform.position, Quaternion.identity,
            spawnPosition.transform);
    }

    private void SpawnBlackCube(GameObject spawnPosition)
    {
        GameObject _blackCube = Instantiate(blackCube, spawnPosition.transform.position, Quaternion.identity,
            spawnPosition.transform);
    }

    IEnumerator LerpColor()
    {
        // Get current colors
        ColorPair currentColorPair = colorPairs[previousColor];

        // Determine which colors we want
        int random = Random.Range(0, colorPairs.Length);
        while (random == previousColor)
        {
            random = Random.Range(0, colorPairs.Length);
        }

        previousColor = random;
        ColorPair wantedColorPair = colorPairs[random];
        Debug.Log($"black = {wantedColorPair.blackColor} // white = {wantedColorPair.whiteColor}");

        float timer = 0;
        while (timer < colorTransitionDuration)
        {
            float t = timer / colorTransitionDuration;
            whiteMaterial.SetColor("_GlowColor",
                Color.Lerp(currentColorPair.whiteColor, wantedColorPair.whiteColor, t));
            blackMaterial.SetColor("_GlowColor",
                Color.Lerp(currentColorPair.blackColor, wantedColorPair.blackColor, t));

            timer += Time.deltaTime;
            yield return null;
        }
        
        // Give wanted colors
        whiteMaterial.SetColor("_GlowColor", wantedColorPair.whiteColor);
        blackMaterial.SetColor("_GlowColor", wantedColorPair.blackColor);
    }

    private IEnumerator ColorSwitch()
    {
        isColorSwitching = true;
        yield return new WaitForSeconds(timeBetweenColorChange);
        // RandomColor();
        StartCoroutine(LerpColor());
        isColorSwitching = false;
    }

    private IEnumerator AccelGame()
    {
        isAccelFinish = true;
        yield return new WaitForSeconds(timeBetweenAccel);
        Debug.Log("Accel !");
        speed += accel;
        scoreMultiplier++;
        accelSpeedEffect += 0.1f;
        speedEffectManager.UpdateParticles(accelSpeedEffect);
        timeMultiplier -= timeMultiplierDecrease;
        isAccelFinish = false;
    }

    private IEnumerator ScoreIncrease()
    {
        isScoreIncrease = true;
        yield return new WaitForSeconds(0.15f);
        score += 1 * scoreMultiplier;
        scoreText.text = score.ToString();
        isScoreIncrease = false;
    }

    private GameObject[] BlackCubeTab()
    {
        return GameObject.FindGameObjectsWithTag("Black");
    }

    private GameObject[] WhiteCubeTab()
    {
        return GameObject.FindGameObjectsWithTag("White");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("ARTHUR");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void CheckIfHighScore()
    {
        if (PlayerPrefs.GetInt("High Score") < score)
        {
            PlayerPrefs.SetInt("High Score", score);
        }
    }

    [System.Serializable]
    struct ColorPair
    {
        [ColorUsage(true, true)] public Color whiteColor;
        [ColorUsage(true, true)] public Color blackColor;
    }
}