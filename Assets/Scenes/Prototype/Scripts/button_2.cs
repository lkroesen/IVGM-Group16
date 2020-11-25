using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_2 : MonoBehaviour
{
    public GameObject A;
    private terminal_main scriptA;
    private void Start()
    {
        A = transform.parent.transform.parent.gameObject;
        scriptA = A.GetComponent<terminal_main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        // scriptA.progressint = 1;
        // Debug.Log(scriptA.progressint);
    }
}
