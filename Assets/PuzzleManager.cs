using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public bool JigsawActive = false;
    public bool bPuzzleActive = false;

    public GameObject bPuzzleReal;
    public GameObject bPuzzleShell;
    public GameObject bPuzzleShell2;
    
    public GameObject house;

    private RemoteController _rc;
    private UI_Text_Handler _uth;

    private void Start()
    {
        var controller = GameObject.FindGameObjectWithTag("Player");
        _rc = controller.GetComponent<RemoteController>();
        _uth = controller.GetComponent<UI_Text_Handler>();
    }

    /**
     * Hides exterior while it is not needed
     */
    public void hideExterior()
    {
        house.SetActive(false);
        
    }

    public void obtainBattery()
    {
        _rc.workingBatteries++;
        batteryText();
    }

    private void batteryText()
    {
        switch (_rc.workingBatteries)
        {
            case 1:
                _uth.BatteriesGet1();
                break;
            case 2:
                _uth.BatteriesGet2();
                break;
        }
    }

    public void syncBPuzzle()
    {
        var go = Instantiate(bPuzzleReal);
        var pos = bPuzzleShell.transform.position;
        var scale = bPuzzleShell.transform.localScale;

        go.transform.position = pos;
        go.transform.localScale = scale;
        
        Destroy(bPuzzleShell);

        bPuzzleShell = go;
    }

    /**
     * Show it again.
     */
    public void showExterior()
    {
        house.SetActive(true);
    }
}
