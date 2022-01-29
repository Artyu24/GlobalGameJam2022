using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Component")] 
    [SerializeField] private Swipe swipeControls;
    [SerializeField] private GameObject leftPosition;
    [SerializeField] private GameObject rightPosition;


    [Header("Parameter")]
    [SerializeField] private float speedTravel;
    [SerializeField] private bool isLeft = false;
    private Vector2 dir;

    private void Update()
    {
        if (swipeControls.swipeLeft && !isLeft)
        {
            dir = leftPosition.transform.position;
            
            isLeft = true;
            Debug.Log("SWIPE LEFT");
        }
        else if (swipeControls.swipeRight && isLeft)
        {
            dir = rightPosition.transform.position;
            isLeft = false;
            Debug.Log("SWIPE RIGHT");
        }
        else if (swipeControls.tap)
        {
            //Debug.Log("Changement de couleur ?");
        }

        transform.position = Vector3.MoveTowards(transform.position, dir, speedTravel);
    }
}
