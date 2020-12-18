using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : MonoBehaviour
{
    // Too glitchy sadly.
    /*
    public bool hasRemote = true;
    public GameObject remote;

    public bool moveUpwards = false;
    public bool moveDown = false;
    public bool isDown = true;
    public bool allowed_to_press = true;
    
    public float speed = 1.0f;

    private GameObject _cam;
    
    private void Start()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (moveDown)
        {
            if (remote.transform.position.y >= _cam.transform.position.y - 0.41f) // x + cameraY = 2.64 there 3.05 - 0.41 = 2.64
            {
                remote.transform.position -= Vector3.up * (Time.deltaTime * speed);
            }
            else
            {
                moveDown = false;
                allowed_to_press = true;
                isDown = true;
            }
        }
        
        if (moveUpwards)
        {
            if (remote.transform.position.y <= _cam.transform.position.y - 0.116f) 
            {
                remote.transform.position += Vector3.up * (Time.deltaTime * speed);
            }
            else
            {
                moveUpwards = false;
                allowed_to_press = true;
                isDown = false;
            }
        }

        if (!hasRemote) return;
        if (!allowed_to_press) return;
        if (Input.GetMouseButton(1))
        {
            if (isDown)
            {
                moveUpwards = true;
            }
            else
            {
                moveDown = true;
            }

            allowed_to_press = false;
        }
    }*/

    public bool firstTimeRemoteShowing = true;
    
    public bool hasRemote = true;
    public GameObject remote;
    public float timeout = 0.1f;
    public float timeout_max = 0.1f;

    public int workingBatteries = 0;
    public bool hasMagnet = false;
    
    private GameObject tutorial_text;

    private UI_Text_Handler _uth;
    
    private void Start()
    {
        tutorial_text = GameObject.FindGameObjectWithTag("TUTORIAL");
        
        var controller = GameObject.FindGameObjectWithTag("Player");
        _uth = controller.GetComponent<UI_Text_Handler>();
        
    }


    void Update()
    {
        if (!hasRemote)
        {
            // TODO: Move this logic to some seperate controls script!
            if (!Input.GetMouseButtonDown(1)) return;
            RealRemoteBaseScript.Return();
            tutorial_text.SetActive(false);
            return;
        }
        if (timeout >= 0)
        {
            timeout -= Time.deltaTime;
            return;
        }

        if (!Input.GetMouseButtonDown(1)) return;
        
        timeout = timeout_max;
        
        
        
        remote.SetActive(!remote.activeSelf);

        if (!firstTimeRemoteShowing) return;
        firstTimeRemoteShowing = false;
        _uth.RemoteTutorial();


    }
}
