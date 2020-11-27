using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteButtonGeneric : MonoBehaviour
{
    public string value = "";

    private MeshRenderer _meshRenderer;
    private Material _material;
    private static readonly int Smoothness = Shader.PropertyToID("_Glossiness");

    private void OnMouseOver()
    {
        _material.SetFloat(Smoothness, 0.409f);
    }

    private void OnMouseUp()
    {
        this.transform.parent.GetComponent<RealRemoteBaseScript>().registerButtonClick(value);
    }

    private void OnMouseExit()
    {
        _material.SetFloat(Smoothness, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
