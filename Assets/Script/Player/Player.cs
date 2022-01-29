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
    [SerializeField] private bool isLeft = true;
    private Vector2 dir;

    [SerializeField] private int health = 3;

    private void Start()
    {
        dir = transform.position;
    }

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

        else if (swipeControls.tap && !swipeControls.isDragging)
        {
            Debug.Log("Changement de couleur ?");
        }

        transform.position = Vector3.MoveTowards(transform.position, dir, speedTravel);
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(1);
        Destroy(other.gameObject);
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
    }
}
