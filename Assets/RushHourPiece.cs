using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;


// TODO: Instead of moving with mouse, just make it "MOVE PIECE BY ONE SQUARE"



[RequireComponent(typeof(Rigidbody))]
public class RushHourPiece : MonoBehaviour
{
    // Whether the piece moves horizontal or if it moves vertical
    public bool horizontal_piece = true;

    // This must be set to true for the piece that triggers the victory e.g. the battery
    public bool isObjective = false;

    public bool activePiece = false;

    public bool colliding = false;

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

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            // Check if other = the goal to end the puzzle.
            case "CAR":
            case "BATTERY":
                colliding = true;
                break;
        }

        Debug.Log(other);
    }

    private void OnCollisionExit(Collision other)
    {
        switch (other.gameObject.tag)
        {
            // Check if other = the goal to end the puzzle.
            case "CAR":
            case "BATTERY":
                colliding = false;
                break;
        }
    }

    private void moveHorizontalPiece()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePoint = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0)) {
            lastMousePoint = null;
        }

        if (lastMousePoint == null) return;

        var diff = Input.mousePosition.x - lastMousePoint.Value;
        var position = transform.position;
        position = new Vector3(position.x + diff * Time.deltaTime, position.y, position.z);
        transform.position = position;
        lastMousePoint = Input.mousePosition.x;
    }

    private void moveVerticalPiece()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePoint = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0)) {
            lastMousePoint = null;
        }

        if (lastMousePoint == null) return;

        var diff = Input.mousePosition.y - lastMousePoint.Value;
        var position = transform.position;
        position = new Vector3(position.x, position.y, position.z + diff * Time.deltaTime);
        transform.position = position;
        lastMousePoint = Input.mousePosition.y;
    }

    // Update is called once per frame
    private float? lastMousePoint = null;
    private void Update()
    {
        if (!activePiece) return;

        if (colliding)
        {
            transform.position = init_pos;
            return;
        }

        if (horizontal_piece)
            moveHorizontalPiece();
        else
            moveVerticalPiece();

    }
}
