using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRemote : MonoBehaviour
{
    public GameObject staticRemote;
    private RemoteController _RemoteController;

    private UI_Text_Handler _uth;
    
    public void Start()
    {
        var controller = GameObject.FindGameObjectWithTag("Player");
        _RemoteController = controller.GetComponent(typeof(RemoteController)) as RemoteController;
        _uth = controller.GetComponent<UI_Text_Handler>();
    }

    private void OnMouseDown()
    {
        _RemoteController.hasRemote = true;
        Destroy(staticRemote);
        _uth.RemoteGet();
    }
}
