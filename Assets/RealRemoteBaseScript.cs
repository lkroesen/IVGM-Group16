using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RealRemoteBaseScript : MonoBehaviour
{
    public GameObject tv;
    public GameObject over_tv;
    
    public void registerButtonClick(string value)
    {
        switch (value)
        {
            case "E" : 
                Return();
                break;
            case "P" : 
                powerButton();
                break;
            case "L" : return;
            case "H" :
                hButton();
                break;
            case "V" : return;
            case "C" : return;
            // Numbers
            case "1" : return;
            case "2" : return;
            case "3" : return;
            case "4" : return;
            case "5" : return;
            case "6" : return;
            case "7" : return;
            case "8" : return;
            case "9" : return;
            case "0" : return;
        }
    }

    void hButton()
    {
        
    }

    public static void Return()
    {
        var _gameObject = GameObject.FindGameObjectWithTag("Player");
        var _cam = GameObject.FindGameObjectWithTag("MainCamera");
        var _wpm = _gameObject.GetComponent<WaypointManager>();
        var _cmm = _cam.GetComponent<CameraMouseMovement>();
        
        if (_cmm.Disable_Camera_Movement)
            PuzzleExit(_cmm, _gameObject);
        
        var wp = _wpm.returnFunc();
        if (wp == null) 
            return;
        else
        {
            _wpm.newWaypoint(wp);
            _cam.transform.position = wp.transform.position;
            _cam.transform.rotation = wp.transform.rotation;
        }
    }

    private static void PuzzleExit(CameraMouseMovement _cmm, GameObject gamecontroller)
    {
        _cmm.Disable_Camera_Movement = false;
        var _pm = gamecontroller.GetComponent<PuzzleManager>();

        if (_pm.bPuzzleActive)
        {
            _pm.bPuzzleActive = false;
            _pm.syncBPuzzle();
        }
        
        _pm.showExterior();

    }

    public bool isTvOn = false;
    private static readonly int COLOR = Shader.PropertyToID("_Color");


    void powerButton()
    {
        over_tv.SetActive(isTvOn);
        isTvOn = !isTvOn;
        
        /*
        var meshRenderer = tv.GetComponent<MeshRenderer>();
        var material = meshRenderer.material;

        if (!isTvOn)
        {
            material.SetColor(COLOR, Color.white);
            isTvOn = true;
        }
        else
        {
            material.SetColor(COLOR, Color.black);
            isTvOn = false;
        }
        */
    }
}
