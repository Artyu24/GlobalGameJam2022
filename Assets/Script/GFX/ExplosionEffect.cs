using System;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private CircleCollider2D circleCollider ;

    private void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1];
        particleSystem.GetParticles(particles);
        float particleSize = particles[0].GetCurrentSize(particleSystem);
        circleCollider.radius = particleSize * 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"collision with {other.name}");
        // Debug.Log(other.tag);
        if (other.CompareTag("Black") || other.CompareTag("White"))
        {
            Destroy(other.gameObject);
        }
    }
}