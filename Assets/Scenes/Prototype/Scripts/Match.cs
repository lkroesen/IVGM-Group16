using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Match : MonoBehaviour
{
    public bool activePiece = false;

    public Rigidbody rigidboy;

    public Vector3 init_pos;
    
    void Start()
    {
        rigidboy = (Rigidbody) GetComponent(typeof(Rigidbody));
        // So that forces do not change our rigidboy
        rigidboy.constraints = RigidbodyConstraints.FreezeAll;
    }

        private void OnMouseDown()
    {
        // Store Position so that when the car collides it returns to its inital position.
        init_pos = transform.position;
        activePiece = true;
    }

    private void OnMouseUp()
    {
        activePiece = false;
    }

    private void MoveMatch()
    {
        float? lastMouseX = null;
        float? lastMouseY = null;

        if (Input.GetMouseButtonDown(0))
        {
            lastMouseX = Input.mousePosition.x;
            lastMouseY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0)) {
            lastMouseX = null;
            lastMouseY = null;
        }

        if (lastMouseX == null && lastMouseY == null) return;
        
        var xdiff = Input.mousePosition.x - lastMouseX.Value;
        var ydiff = Input.mousePosition.y - lastMouseY.Value;
        var position = transform.position;
        position = new Vector3(position.x + xdiff * Time.deltaTime, position.y, position.z + ydiff * Time.deltaTime);
        transform.position = position;
        lastMouseX = Input.mousePosition.x;
        lastMouseY = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(!activePiece) return;

        else MoveMatch();
    }
}
