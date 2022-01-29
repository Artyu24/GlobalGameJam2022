using System;
using System.ComponentModel;
using UnityEngine;

public class SpeedEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    [SerializeField] private MinMaxAndCurrent particlesSpeed;
    [SerializeField] private MinMaxAndCurrent particlesCount;
 
    public void SetParticleSpeed(float t)
    {
        // Define new speed
        particlesSpeed.SetT(t);
        float newSpeed = particlesSpeed.GetCurrentValue();
        
        // Change speed for future particles
        var particleSystemMain = particleSystem.main;
        particleSystemMain.startSpeed = newSpeed;

        // Change speed for current particles
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        particleSystem.GetParticles(particles);
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].velocity = particles[i].velocity.normalized * newSpeed;
        }
        particleSystem.SetParticles(particles);
    }

    public void SetParticlesCount(float t)
    {
        // Define new particles count
        particlesCount.SetT(t);
        int newCount = (int)particlesCount.GetCurrentValue();

        // Set new particles rate over time
        var particleSystemEmission = particleSystem.emission;
        particleSystemEmission.rateOverTime = newCount;
    }

    [System.Serializable]
    struct MinMaxAndCurrent
    {
        [Tooltip("Hand edit uselesss")]
        public float t;
        public Vector2 minMax;

        public void SetT(float newCurrent) => t = Mathf.Min(1f, newCurrent);
        public float GetCurrentValue() => minMax.x + (minMax.y - minMax.x) * t;
    }
}