using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bug in this code I'm aware off:
 * - Pieces can move through objects that are only halfway in the way,
 * however, I could fix this, by doing the raycasts from the outter sides, however,
 * I do not think this will affect any puzzle solution, so I'm keeping it in.
 */

public class RushHourPiece : MonoBehaviour
{
    public float?[] limit = new float?[2];
    
    private Rigidbody _rigidbody;

    private Vector3 actual_location;
    private Vector3 difference;

    public bool vertical_piece;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    float distanceBetween(float a, float b)
    {
        return Mathf.Abs(a - b);
    }

    void setLimits()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        
        if (vertical_piece)
        {
            Physics.Raycast(transform.position, Vector3.left, out var hit, Mathf.Infinity, layerMask);

            if (hit.collider == null) limit[0] = Mathf.NegativeInfinity;
            else if (hit.collider.CompareTag("bp_piece"))
            {
                Physics.Raycast(hit.point, Vector3.right, out var ret_hit, Mathf.Infinity,
                    layerMask);
                
                var distance = distanceBetween(hit.point.x, ret_hit.point.x);
                distance *= 0.95f;
                
                limit[0] = (transform.position.x - distance);
            }
            else
            {
                limit[0] = Mathf.NegativeInfinity;
            }
            
            Physics.Raycast(transform.position, Vector3.right, out var _hit, Mathf.Infinity, layerMask);

            if (_hit.collider == null) limit[1] = Mathf.Infinity;
            else if (_hit.collider.CompareTag("bp_piece"))
            {
                // Calculate the actual distance between the outsides of the two objects.
                
                // We found the first side of the cube with the first Raycast, now we raycast back from
                // the object into our original object to find out the object's edge
                Physics.Raycast(_hit.point, Vector3.left, out var ret_hit, Mathf.Infinity,
                    layerMask);
                
                // Calculate the distance between our two hit points
                var distance = distanceBetween(_hit.point.x, ret_hit.point.x);
                
                // Tolerance to keep some distance between the objects
                // (Because without this the Raycast goes through the object we detected)
                distance *= 0.95f;
                
                // Set the limit based on the distance from the x-origin and the distance.
                limit[1] = (transform.position.x + distance);
            }
            else
            {
                limit[1] = null;
            }
        }
        else
        {
            Physics.Raycast(transform.position, Vector3.forward, out var hit, Mathf.Infinity, layerMask);

            if (hit.collider == null) limit[0] = Mathf.Infinity;
            else if (hit.collider.CompareTag("bp_piece"))
            {
                Physics.Raycast(hit.point, Vector3.back, out var ret_hit, Mathf.Infinity,
                    layerMask);
                
                var distance = distanceBetween(hit.point.z, ret_hit.point.z);
                distance *= 0.95f;
                
                limit[0] = (transform.position.z + distance);
            }
            else
            {
                limit[0] = Mathf.Infinity;
            }
            
            Physics.Raycast(transform.position, Vector3.back, out var _hit, Mathf.Infinity, layerMask);
            
            if (_hit.collider == null) limit[1] = Mathf.NegativeInfinity;
            else if (_hit.collider.CompareTag("bp_piece"))
            {
                
                
                Physics.Raycast(_hit.point, Vector3.forward, out var ret_hit, Mathf.Infinity,
                    layerMask);
                
                var distance = distanceBetween(_hit.point.z, ret_hit.point.z);
                distance *= 0.95f;
                
                limit[1] = (transform.position.z - distance);
            }
            else
            {
                limit[1] = Mathf.NegativeInfinity;
            }
        }
        
        Debug.Log("Limit: " + limit[0] + " <> " + limit[1]);
    }

    private void OnMouseDown()
    {
        setLimits();
        
        var position = transform.position;
        if (Camera.main == null) return;
        
        actual_location = Camera.main.WorldToScreenPoint(position);
        difference = position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, actual_location.z));
    }

    private void OnMouseDrag()
    {
var cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, actual_location.z);
        if (Camera.main == null) return;
        var cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + difference;

        cursorPosition.x = vertical_piece ? cursorPosition.x : transform.position.x;
        cursorPosition.z = vertical_piece ? transform.position.z : cursorPosition.z;
        cursorPosition.y = transform.position.y;
        
        
        if (vertical_piece)
        {
            if (!(cursorPosition.x <= limit[1]) || !(cursorPosition.x >= limit[0])) return;
            transform.position = cursorPosition;
        }
        else
        {
            if (!(cursorPosition.z >= limit[1]) || !(cursorPosition.z <= limit[0])) return;
            transform.position = cursorPosition;
        }
    }

}
