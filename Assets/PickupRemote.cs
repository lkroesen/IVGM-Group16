using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRemote : MonoBehaviour
{
    public GameObject staticRemote;
    public RemoteController _RemoteController;

    public void Start()
    {
        _RemoteController = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(RemoteController)) as RemoteController;
    }

    private void OnMouseDown()
    {
        _RemoteController.hasRemote = true;
        Destroy(staticRemote);
    }
}
