using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Bug in this code I'm aware off:
 * - Pieces can move through objects that are only halfway in the way,
 * however, I could fix this, by doing the raycasts from the outter sides, however,
 * I do not think this will affect any puzzle solution, so I'm keeping it in.
 */

public class RushHourPiece : MonoBehaviour
{
    public float[] limit = new float[2];

    public bool isLeftBattery;
    public bool isRightBattery;
    private bool reachedGoal = false;
    
    private Rigidbody _rigidbody;
    private BatteryPuzzleManager _bpm;

    private Vector3 actual_location;
    private Vector3 difference;

    public bool vertical_piece;

    private float xScale;
    private float zScale;

    private Vector3 stored_pos;

    private bool colliding = false;

    private void OnCollisionStay(Collision other)
    {
        if (_bpm.moveOut || _bpm.savePositions)
        {
            stored_pos = transform.position;
            return;
        }
        
        if (isLeftBattery)
            if (other.collider.gameObject.CompareTag("bp_goal"))
            {
                return;
            }

        colliding = true;
        transform.position = stored_pos;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (isLeftBattery)
            if (other.collider.gameObject.CompareTag("bp_goal"))
            {
                return;
            }
        
        colliding = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (isLeftBattery)
            if (other.collider.gameObject.CompareTag("bp_goal"))
            {
                return;
            }
        colliding = false;
    }

    private void Start()
    {
        _bpm = transform.parent.transform.parent.transform.parent.GetComponent<BatteryPuzzleManager>();
        
        stored_pos = transform.position;
        xScale = transform.localScale.x;
        zScale = transform.localScale.z;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    float distanceBetween(float a, float b)
    {
        return Mathf.Abs(a - b);
    }

    float vectCount(Vector3 input)
    {
        return input.x + input.y + input.z;
    }

    /**
     * Casts ray in direction from an origin, returns the distance between the origin object and the first hit object,
     * if no object it returns infinity in the direction cast.
     *
     * useX to signify direction.
     */
    float castRay(Vector3 origin, Vector3 direction, bool useX)
    {
        var layerMask = 1 << 8;
        layerMask = ~layerMask;

        var vEffective = vectCount(direction);
        
        Physics.Raycast(origin, direction, out var hit, Mathf.Infinity, layerMask);

        if (hit.collider == null) return vEffective <= -1 ? Mathf.NegativeInfinity : Mathf.Infinity;

        if (!hit.collider.CompareTag("bp_piece")) return vEffective <= -1 ? Mathf.NegativeInfinity : Mathf.Infinity;
        
        // Calculate the actual distance between the outsides of the two objects.
                
        // We found the first side of the cube with the first Raycast, now we raycast back from
        // the object into our original object to find out the object's edge
        Physics.Raycast(hit.point, -1 * direction, out var ret_hit, Mathf.Infinity,
            layerMask);
            
        // Calculate the distance between our two hit points  
        var distance = 0f;
        distance = useX ? distanceBetween(hit.point.x, ret_hit.point.x) : distanceBetween(hit.point.z, ret_hit.point.z);
        
        // Tolerance to keep some distance between the objects
        // (Because without this the Raycast goes through the object we detected)
        distance *= 0.95f;
            
        // Set the limit based on the distance from the x-origin and the distance.
        var position = transform.position;
        return useX ? position.x + (distance * vEffective): position.z + (distance * vEffective);
    }

    float multipleRays(Vector3 origin, Vector3 direction, bool useX)
    {
        Vector3 upper_origin;
        float upper_distance;
        Vector3 lower_origin;
        float lower_distance;
        float distance;
        var vEffective = vectCount(direction);
        
        if (useX)
        {
            upper_origin = origin;
            upper_origin.z += zScale / 2.0f;

            upper_distance = castRay(upper_origin, direction, true);
            
            lower_origin = origin;
            lower_origin.z -= zScale / 2.0f;

            lower_distance = castRay(lower_origin, direction, true);

            distance = castRay(origin, direction, true);

            return vEffective <= -1 ? Mathf.Max(Math.Max(upper_distance, lower_distance), distance) : Math.Min(Math.Min(upper_distance, lower_distance), distance);
        }

        upper_origin = origin;
        upper_origin.x += xScale / 2.0f;

        upper_distance = castRay(upper_origin, direction, false);
            
        lower_origin = origin;
        lower_origin.x -= xScale / 2.0f;

        lower_distance = castRay(lower_origin, direction, false);

        distance = castRay(origin, direction, false);

        return vEffective <= -1 ? Mathf.Max(Math.Max(upper_distance, lower_distance), distance) : Math.Min(Math.Min(upper_distance, lower_distance), distance);
    }
    
    void setLimits()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        
        if (vertical_piece)
        {
            // Casts rays from 3 locations to ensure maximum detection
            /* If any ray hits an object it means we can only move to the maximum of that location
             *     V-- Use this limit
             *     | ----|        |-------- inf
             * |---------| origin |-----|
             * inf-------|        |-----|
             *                          ^-- Use this limit
             */

            limit[0] = multipleRays(transform.position, Vector3.left, true);//castRay(transform.position, Vector3.left, true);

            limit[1] = multipleRays(transform.position, Vector3.right, true);
        }
        else
        {
            limit[0] = castRay(transform.position, Vector3.forward, false);
            limit[1] = castRay(transform.position, Vector3.back, false);
        }
    }

    private float timeout = 0.3f;
    public float timeout_max = 0.3f;
    
    private void Update()
    {
        timeout -= Time.deltaTime;

        if (_bpm.savePositions)
        {
            stored_pos = transform.position;
        }
        
    }

    private void OnMouseDown()
    {
        if (timeout >= 0) return;
        timeout = timeout_max;

        setLimits();
        
        var position = transform.position;
        if (Camera.main == null) return;
        
        actual_location = Camera.main.WorldToScreenPoint(position);
        difference = position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, actual_location.z));
    }

    private void OnMouseUp()
    {
        if (!colliding)
            stored_pos = transform.position;
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
            // If the cursor goes further than the block can go, set the block to be at the edge.
            if (!(cursorPosition.x <= limit[1]))
                cursorPosition.x = limit[1];
            else if (!(cursorPosition.x >= limit[0]))
                cursorPosition.x = limit[0];
            transform.position = cursorPosition;
            
            if (isLeftBattery || isRightBattery)
            {
                checkGoalReached();
            }
        }
        else
        {
            //if (!(cursorPosition.z >= limit[1]) || !(cursorPosition.z <= limit[0])) return;
            if (!(cursorPosition.z >= limit[1]))
                cursorPosition.z = limit[1];
            else if (!(cursorPosition.z <= limit[0]))
                cursorPosition.z = limit[0];
            transform.position = cursorPosition;
        }
    }

    public GameObject goal_indicator;

    private void checkGoalReached()
    {
        if (reachedGoal) return;
        
        if (isLeftBattery)
        {
            if (!(transform.position.x > goal_indicator.transform.position.x)) return;
            reachedGoal = true;
            Debug.Log("Left Battery Reached the goal!");
        }
        else
        {
            if (!(transform.position.x < goal_indicator.transform.position.x)) return;
            reachedGoal = true;
            Debug.Log("Right Battery Reached the goal!");
        }

        
    }

}
