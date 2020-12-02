using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flip : MonoBehaviour
{
    public GameObject A;
    private switch_script scriptA;
    private void Start()
    {
        //Allows for calling main scipt.
        A = transform.parent.gameObject;
        scriptA = A.GetComponent<switch_script>();
        //Default angle
        gameObject.transform.RotateAround(gameObject.transform.parent.position, Vector3.forward, -45.0f);
    }

    private void OnMouseDown() {
        //Call main function.
        scriptA.flip(gameObject);
    }
}

