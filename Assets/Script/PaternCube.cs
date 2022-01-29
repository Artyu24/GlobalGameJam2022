using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaternCube : MonoBehaviour
{
    public List<PaternLayout> listPatern;

    [System.Serializable]
    public struct PaternLayout
    {
        public GameObject leftCube;
        public GameObject rightCube;
        public float timeBetweenThem;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Patern());
        }
    }

    public IEnumerator Patern()
    {
        foreach (PaternLayout layout in listPatern)
        {
            GameManager.Instance.SpawnCube("left", layout.leftCube);
            GameManager.Instance.SpawnCube("right", layout.rightCube);
            yield return new WaitForSeconds(layout.timeBetweenThem + .3f);
        }
    }
}
