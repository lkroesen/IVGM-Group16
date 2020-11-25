using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminal_main : MonoBehaviour
{
    GameObject screen1;
    GameObject screen3;
    GameObject screen6;
    public int progressint;
    int [] array_sequence = new int[3] {1,3,6};
    // Start is called before the first frame update
    void Start()
    {
        progressint = 0;
    }


    void ChangeColor(GameObject obj, Color c){
        var Renderer = obj.GetComponent<Renderer>();
        Renderer.material.SetColor("_Color", c);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < array_sequence.Length; i++){
            string screennumber = ("screen"+array_sequence[i].ToString());
            Debug.Log(screennumber);
            GameObject obj = GameObject.Find(screennumber);
            ChangeColor(obj, Color.blue);
        }
    }
}
