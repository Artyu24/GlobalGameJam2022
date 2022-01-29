using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance { get => _instance; }

    [SerializeField] private float speed;
    public float GetSpeed { get => speed; }

    [SerializeField] private GameObject leftSpawn, rightSpawn;
    [SerializeField] private GameObject blackCube, whiteCube;

    [SerializeField] private PaternCube[] tabPatern;

    private bool isPaternPlay;
    public bool GetIsPaternPlay { get { return isPaternPlay; } set { isPaternPlay = value; } }

    private bool isWhiteSide;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (!isPaternPlay)
        {
            PlayRandomPatern();
        }   
    }

    private void PlayRandomPatern()
    {
        int random = Random.Range(0, tabPatern.Length);
        Debug.Log("Patern : " + random);
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
        GameObject _whiteCube = Instantiate(whiteCube, spawnPosition.transform.position, Quaternion.identity);
        if (isWhiteSide)
            _whiteCube.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void SpawnBlackCube(GameObject spawnPosition)
    {
        GameObject _blackCube = Instantiate(blackCube, spawnPosition.transform.position, Quaternion.identity);
        if (!isWhiteSide)
            _blackCube.GetComponent<BoxCollider2D>().enabled = false;
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
