using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class movePiece : MonoBehaviour
{
    private Vector3 collider_pos;

    private bool inCollider = false;

    public float speed = 0.1f;

    private Rigidbody _rigidbody;
    
    // Update is called once per frame
    private float? lastMousePoint_x = null;
    private float? lastMousePoint_y = null;

    private Vector3 actual_location;
    private Vector3 difference;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

    void OnMouseDown()
    {
        var position = transform.position;
        if (Camera.main == null) return;
        actual_location = Camera.main.WorldToScreenPoint(position);
        difference = position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, actual_location.z));
    }
		
    void OnMouseDrag()
    {
        var cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, actual_location.z);
        if (Camera.main == null) return;
        var cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + difference;
        transform.position = cursorPosition;
    }

    private void OnMouseUp()
    {
        if (inCollider != true) return;
        transform.position = collider_pos;
        inCollider = false;
    }

    [Obsolete("This method is for giving an object speed rather than conforming to a mouse position")]
    private void movePieceXY()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
          lastMousePoint_x = Input.mousePosition.x;
          lastMousePoint_y = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0)) {
            lastMousePoint_x = null;
            lastMousePoint_y = null;
        }

        if (lastMousePoint_x == null) return;
        if (lastMousePoint_y == null) return;

        var diff_x = Input.mousePosition.x - lastMousePoint_x.Value;
        var diff_y = Input.mousePosition.y - lastMousePoint_y.Value;
        var position = transform.position;
        position = new Vector3(position.x + 5*diff_x*Time.deltaTime*speed, position.y, position.z + 5*diff_y * Time.deltaTime * speed);
        transform.position = position;
        lastMousePoint_y = Input.mousePosition.y;
        lastMousePoint_x = Input.mousePosition.x;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("PIECE"))
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = true;
        collider_pos = other.transform.position;
      }
    }

    private void OnTriggerStay(Collider other)
    {
      if (other.CompareTag("PIECE"))
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = true;
        collider_pos = other.transform.position;
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag("PIECE"))
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = false;
        collider_pos = other.transform.position;
      }
    }
}
