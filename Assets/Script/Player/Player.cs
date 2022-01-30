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

    [Header("Health")]
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject[] tabHeart = new GameObject[3];

    [Header("Effects"), SerializeField] private GameObject explosionEffectPrefab;
    
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
            GameManager.Instance.SwitchLine();
        }
        else if (swipeControls.swipeRight && isLeft)
        {
            dir = rightPosition.transform.position;
            isLeft = false;
            Debug.Log("SWIPE RIGHT");
            GameManager.Instance.SwitchLine();
        }

        else if (swipeControls.tap && !swipeControls.isDragging)
        {
            Debug.Log("Changement de couleur ?");
        }

        transform.position = Vector3.MoveTowards(transform.position, dir, speedTravel);

        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.isPlayerAlive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TakeDamage(1);
        GameManager.Instance.ResetCube();
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if (OptionManager.instance.isVibrationEnabled)
        {
            Handheld.Vibrate();
        }
        tabHeart[health].GetComponent<Animator>().SetTrigger("Break");
        Debug.Log(health);
        Destroy(Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity), 1f);
    }
}
