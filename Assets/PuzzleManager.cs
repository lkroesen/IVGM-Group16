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
    /**
     * Hides exterior while it is not needed
     */
    public void hideExterior()
    {
        house.SetActive(false);
        
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
