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

    public void ActivateCoroutine()
    {
        StartCoroutine(Patern());
    }

    private IEnumerator Patern()
    {
        GameManager.Instance.GetIsPaternPlay = true;
        foreach (PaternLayout layout in listPatern)
        {
            GameManager.Instance.SpawnCube("left", layout.leftCube);
            GameManager.Instance.SpawnCube("right", layout.rightCube);
            yield return new WaitForSeconds((layout.timeBetweenThem + .3f) * GameManager.Instance.GetTimeMultiplier);
        }

        yield return new WaitForSeconds(.6f);
        GameManager.Instance.GetIsPaternPlay = false;
    }
}
