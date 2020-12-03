using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPuzzleManager : MonoBehaviour
{
    public GameObject outterwaypoint;
    public float speed = 0.1f;
    public bool moveOut = false;
    public bool savePositions = false;
    public float timer = 1.0f;
    public bool savedPositions = false;
    private bool hasMoved = false;

    private GameObject wp;

    private PuzzleManager _pm;
    
    private void Start()
    {
        wp = GameObject.FindGameObjectWithTag("bp_wp");
        _pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PuzzleManager>();
    }

    public void leftBatteryObtain()
    {
        _pm.obtainBattery();
    }

    public void rightBatteryObtain()
    {
        _pm.obtainBattery();
    }
    
    private void Update()
    {
        if (moveOut && !hasMoved)
        {
            if (transform.position.z <= outterwaypoint.transform.position.z)
            {
                moveOut = false;
                hasMoved = true;
                savePositions = true;
                wp.transform.localPosition = new Vector3(-26.097f, -14.506f, 12.5f);
            }
            transform.position += Vector3.back * (speed * Time.deltaTime);
        }

        if (!savePositions) return;
        if (timer <= 0)
        {
            savePositions = false;
            savedPositions = true;
        }
        timer -= Time.deltaTime;
    }
}
