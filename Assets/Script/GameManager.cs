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

    private bool isWhiteSide;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnBlackCube(leftSpawn);
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnWhiteCube(rightSpawn);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnBlackCube(rightSpawn);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnWhiteCube(leftSpawn);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchLine();
        }
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
