using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkip : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        this.gameObject.SetActive(false);
    }
}
