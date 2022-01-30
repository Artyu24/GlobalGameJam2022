using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trailRenderer;
    
    private void Start()
    {
        dir = transform.position;
        OptionManager.instance.SwitchSound();
        OptionManager.instance.SwitchVibration();
        // Get shared material
    }

    private void Update()
    {
        if (swipeControls.swipeLeft && !isLeft)
        {
            dir = leftPosition.transform.position;
            
            isLeft = true;
            Debug.Log("SWIPE LEFT");
            GameManager.Instance.SwitchLine();
            UpdateTrailColor();
        }
        else if (swipeControls.swipeRight && isLeft)
        {
            dir = rightPosition.transform.position;
            isLeft = false;
            Debug.Log("SWIPE RIGHT");
            GameManager.Instance.SwitchLine();
            UpdateTrailColor();
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

    void UpdateTrailColor()
    {
        // Update trail color with the current color of the player
        Color color = spriteRenderer.sharedMaterial.GetColor("_GlowColor");
        trailRenderer.startColor = color;
        trailRenderer.endColor = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameManager.Instance.getActualTag))
            TakeDamage(1);
        // GameManager.Instance.ResetCube();
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
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }
}
