using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MovementCube : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float verticalMov = speed * Time.fixedDeltaTime * -1;
        Movement(verticalMov);
    }

    private void Movement(float _verticalMov)
    {
        Vector3 targetVelocity = new Vector2(0, _verticalMov);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    }
}
