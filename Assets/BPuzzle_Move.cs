using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPuzzle_Move : MonoBehaviour
{
    private BatteryPuzzleManager _bpm;
    
    private void Start()
    {
        _bpm = transform.parent.gameObject.transform.parent.gameObject.GetComponent<BatteryPuzzleManager>();
    }

    private void OnMouseUpAsButton()
    {
        _bpm.moveOut = true;
    }
}
