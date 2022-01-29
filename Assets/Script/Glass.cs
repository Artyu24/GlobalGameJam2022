using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider2D;
    
    public void Break()
    {
        animator.SetTrigger("break");

        if (collider2D != null)
        {
            if (!collider2D.isTrigger) collider2D.enabled = false;
        }
    }
}
