using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MagnetAqcuire : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Debug.Log("Click?");
        var controller = GameObject.FindGameObjectWithTag("Player");
        var _uth = controller.GetComponent<UI_Text_Handler>();
        _uth.winMatchPuzzle();
        Destroy(this.transform.parent.gameObject);
    }
}
