using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorial;

    private void OnMouseUp()
    {
        tutorial.SetActive(true);
    }
}
