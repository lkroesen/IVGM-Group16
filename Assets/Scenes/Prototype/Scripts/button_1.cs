﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_1 : MonoBehaviour
{
    public GameObject A;
    private terminal_main scriptA;
    private void Start(){
        A = transform.parent.transform.parent.gameObject;
        scriptA = A.GetComponent<terminal_main>();
    }

    private void OnMouseDown() {
        scriptA.ButtonPress(gameObject.name);
    }
}