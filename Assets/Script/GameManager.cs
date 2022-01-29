using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance { get => _instance; }

    [Header("Vitesse des cubes")]
    [SerializeField] private float speed;
    public float GetSpeed { get => speed; }

    [Header("Accélération")]
    private float accel;
    [SerializeField] private float timeBetweenAccel;
    
    [Header("Réduction du temps à chaque accel (<0.1f)")]
    [SerializeField] private float timeMultiplierDecrease;
    private float timeMultiplier;
    public float GetTimeMultiplier { get { return timeMultiplier; } }

    [Header("Spawn & Cubes")]
    [SerializeField] private GameObject leftSpawn, rightSpawn;
    [SerializeField] private GameObject blackCube, whiteCube;

    [Header("Tableau des paternes")]
    [SerializeField] private PaternCube[] tabPatern;

    [Header("Score")]
    private int score;
    private int scoreMultiplier;
    [SerializeField] private Text scoreText;

    [Header("Material")] 
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;

    private bool isPaternPlay;
    public bool GetIsPaternPlay { get { return isPaternPlay; } set { isPaternPlay = value; } }

    private bool isWhiteSide;
    private bool isAccelFinish;
    private bool isScoreIncrease;

    private void Awake()
    {
        _instance = this;

        scoreMultiplier = 1;
        scoreText.text = score.ToString();
        timeMultiplier = 1;
        accel = speed * 0.2f;

        blackMaterial.SetColor("_GlowColor", Color.black);
        blackMaterial.SetColor("_BaseColor", Color.black);
        whiteMaterial.SetColor("_GlowColor", Color.white);
        whiteMaterial.SetColor("_BaseColor", Color.black);
    }

    private void Update()
    {
        if (!isAccelFinish && speed < accel * 15)
            StartCoroutine(AccelGame());


        if (!isScoreIncrease)
            StartCoroutine(ScoreIncrease());

        if (!isPaternPlay)
            PlayRandomPatern();
    }

    private void PlayRandomPatern()
    {
        int random = Random.Range(0, tabPatern.Length);
        tabPatern[random].ActivateCoroutine();
    }

    public void SwitchLine()
    {
        isWhiteSide = !isWhiteSide;

        foreach (GameObject _blackCube in BlackCubeTab())
        {
            _blackCube.GetComponent<BoxCollider2D>().enabled = isWhiteSide;
        }

        foreach (GameObject _whiteCube in WhiteCubeTab())
        {
            _whiteCube.GetComponent<BoxCollider2D>().enabled = !isWhiteSide;
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
        GameObject _whiteCube = Instantiate(whiteCube, spawnPosition.transform.position, Quaternion.identity, spawnPosition.transform);
        if (isWhiteSide)
            _whiteCube.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void SpawnBlackCube(GameObject spawnPosition)
    {
        GameObject _blackCube = Instantiate(blackCube, spawnPosition.transform.position, Quaternion.identity, spawnPosition.transform);
        if (!isWhiteSide)
            _blackCube.GetComponent<BoxCollider2D>().enabled = false;
    }

    private IEnumerator AccelGame()
    {
        isAccelFinish = true;
        yield return new WaitForSeconds(timeBetweenAccel);
        Debug.Log("Accel !");
        speed += accel;
        scoreMultiplier++;
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
}
