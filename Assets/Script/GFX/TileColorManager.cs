using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _material.SetColor("_GlowColor", Color.green);
        Debug.Log(_material.GetColor("_GlowColor"));
    }

    // Update is called once per frame
    void Update()
    {
    }
}