using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHourPiece : MonoBehaviour
{
    private Vector3 collider_pos;
    private Rigidbody _rigidbody;

    private Vector3 actual_location;
    private Vector3 difference;

    public float unstuck_speed;
    public bool vertical_piece;

    private bool active = false;
    private bool unstucking = false;
    private bool hit = false;

    private float direction;

    private BatteryPuzzleManager _bpm;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _bpm = transform.parent.gameObject.GetComponent<BatteryPuzzleManager>();
    }

    private void OnMouseUp()
    {
        active = false;
    }

    void OnMouseDown()
    {
        active = true;
        _bpm.canMove = true;
        
        var position = transform.position;
        if (Camera.main == null) return;
        
        actual_location = Camera.main.WorldToScreenPoint(position);
        difference = position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, actual_location.z));
    }
		
    void OnMouseDrag()
    {
        if (!_bpm.canMove) return;
        if (hit) return;
        if (unstucking) return;

        var cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, actual_location.z);
        if (Camera.main == null) return;
        var cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + difference;

        cursorPosition.x = vertical_piece ? cursorPosition.x : transform.position.x;
        cursorPosition.z = vertical_piece ? transform.position.z : cursorPosition.z;
        cursorPosition.y = transform.position.y;

        var mov = transform.position - cursorPosition;
        direction = vertical_piece ? mov.x : mov.z;  
        
        transform.position = cursorPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (vertical_piece)
        {
            if (direction < 0.0)
            {
                Debug.Log("Moved forewards");
            }
            else if (direction > 0.0)
            {
                Debug.Log("Moved backwards");
            }
            else
            {
                Debug.Log("We got hit");
            }
        }
        else
        {
            if (direction > 0.0)
            {
                Debug.Log("Moved forewards");
            }
            else if (direction < 0.0)
            {
                Debug.Log("Moved backwards");
            }
            else
            {
                Debug.Log("We got hit");
            }
        }
        
        hit = true;
    }

    private void OnMouseOver()
    {
        if (!active)
            _bpm.canMove = false;

    }

    private void OnTriggerExit(Collider other)
    {
        hit = false;
        unstucking = false;
    }

    private void OnTriggerStay(Collider other)
    {
        unstucking = true;
        
        var unstuck = transform.position;

        unstuck.x += vertical_piece ? direction * Time.deltaTime * unstuck_speed : 0;
        unstuck.z += vertical_piece ? 0 : direction * Time.deltaTime * unstuck_speed;
        
        transform.position = unstuck;
    }
}
