using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeRemote : MonoBehaviour
{
    public float mod;
    public bool clicked;
    public AudioSource _as;

    private Material _material;
    private static readonly int Smoothness = Shader.PropertyToID("_Glossiness");

    
    // Start is called before the first frame update
    void Start()
    {
        _material = transform.parent.gameObject.GetComponent<MeshRenderer>().material;
    }
    
    private void OnMouseOver()
    {
        _material.SetFloat(Smoothness, 0.409f);
    }
    
    private void OnMouseExit()
    {
        _material.SetFloat(Smoothness, 0f);
    }

    private void OnMouseDown()
    {
        clicked = true;
    }

    private void OnMouseUp()
    {
        clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            _as.volume += mod * Time.deltaTime;
        }
    }
}
