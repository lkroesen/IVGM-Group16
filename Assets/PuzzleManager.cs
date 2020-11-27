using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public bool JigsawActive = false;

    public GameObject house;
    /**
     * Hides exterior while it is not needed
     */
    public void hideExterior()
    {
        house.SetActive(false);
        
    }

    /**
     * Show it again.
     */
    public void showExterior()
    {
        house.SetActive(true);
    }
}
