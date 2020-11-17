using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHour : MonoBehaviour
{
    public bool isActive = true;
    private CameraMouseMovement _cmm;

    // 0 - Empty Space
    // 1 - Battery
    // 2 - Car
    // 3 - Truck
    public int[,] board = new int[6,6];
    
    // Start is called before the first frame update
    void Start()
    {
        // Get Our Camera
        _cmm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMouseMovement>();
        _cmm.Disable_Camera_Movement = true;
        
        
    }

    void loadPuzzle(int n)
    {
        string puzzleOne = "0 0 0 0 0 0\n1 1 0 0 0 0\n0 0 0 0 0 0\n0 0 0 0 0 0\n0 0 0 0 0 0\n0 0 0 0 0 0";
        var parts = puzzleOne.Split(' ');
        
    }

    
    
    // Update is called once per frame
    void Update()
    {
    }
}
