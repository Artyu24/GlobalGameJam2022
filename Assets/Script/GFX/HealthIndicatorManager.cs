using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthIndicatorManager : MonoBehaviour
{
    public static HealthIndicatorManager instance;

    [SerializeField] private Animator _animator;
    private Vignette _vignette;

    [Range(0,1)]
    public float vignetteIntensity;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void BlinkHealthIndicator()
    {
        _animator.SetTrigger("blink");    
    }

    public void EnableHealthIndicator()
    {
        _animator.SetTrigger("enable");   
    }

    public void DisableHealthIndicator()
    {
        _animator.SetTrigger("disable");
    }

    private void Start()
    {
        Volume volume = GetComponent<Volume>();
        if (volume.profile.Has<Vignette>())
        {
            foreach (VolumeComponent profileComponent in volume.profile.components)
            {
                if(profileComponent is Vignette vignette)
                {
                    _vignette = vignette;
                    // _vignette.intensity = new ClampedFloatParameter(0, 0, 1f, false);
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        _vignette.intensity = new ClampedFloatParameter(vignetteIntensity, 0f, 1f, true);
    }
}