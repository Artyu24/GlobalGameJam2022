using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaternCube : MonoBehaviour
{
    [Header("Line 1")]
    [SerializeField] private GameObject leftCube_1;
    [SerializeField] private GameObject rightCube_1;
    [SerializeField] private float timeBetweenThem_1;
    [Header("Line 2")]
    [SerializeField] private GameObject leftCube_2;
    [SerializeField] private GameObject rightCube_2;
    [SerializeField] private float timeBetweenThem_2;
    [Header("Line 3")]
    [SerializeField] private GameObject leftCube_3;
    [SerializeField] private GameObject rightCube_3;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Patern());
        }
    }

    public IEnumerator Patern()
    {
        GameManager.Instance.SpawnCube("left", leftCube_1);
        GameManager.Instance.SpawnCube("right", rightCube_1);
        yield return new WaitForSeconds(timeBetweenThem_1 + .3f);
        GameManager.Instance.SpawnCube("left", leftCube_2);
        GameManager.Instance.SpawnCube("right", rightCube_2);
        yield return new WaitForSeconds(timeBetweenThem_2 + .3f);
        GameManager.Instance.SpawnCube("left", leftCube_3);
        GameManager.Instance.SpawnCube("right", rightCube_3);
    }
}
