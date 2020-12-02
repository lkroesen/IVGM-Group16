using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public readonly Stack waypointStack = new Stack();
    public GameObject wp_home;
    
    private void Start()
    {
        newWaypoint(wp_home);
    }

    /**
     * VERY IMPORTANT!
     * EVERY CAM POSITIONAL UPDATE SHOULD ADD A WAYPOINT
     */
    public void newWaypoint(GameObject wp)
    {
        waypointStack.Push(wp);
    }

    /**
     * Manages the stack incase of using the "return" function.
     *
     * Example:
     * Start -> Vault
     *
     * Stack:
     * Start
     * Vault : stPointer
     *
     * Pop current and make active, pop previous, deactivate and go to location.
     *
     * However, if we're at the Start (stack size = 1) return null, as we can't move to a previous location.
     */
    public GameObject returnFunc()
    {
        if (waypointStack.Count == 1)
            return null;
        
        var _wp = waypointStack.Pop() as GameObject;
        if (_wp != null) _wp.SetActive(true);
        _wp = waypointStack.Pop() as GameObject;
        if (_wp != null) _wp.SetActive(false);
        return _wp;
    }
    
    public void activeLastWaypoint()
    {
        var wp = waypointStack.Peek() as GameObject;
        if (wp != null) wp.SetActive(true);
    }
}
