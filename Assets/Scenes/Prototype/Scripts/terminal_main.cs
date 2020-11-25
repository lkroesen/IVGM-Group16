using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminal_main : MonoBehaviour
{
    //Code
    private int [] code = new int[4]{3,2,1,4};
    
    //Keeping track of progress
    private int correct;
    public bool done;


    void ChangeColor(GameObject obj, Color c){
        var Renderer = obj.GetComponent<Renderer>();
        Renderer.material.SetColor("_Color", c);
    }

    void Start()
    {
        done = false;
        Fail();
    }

    public void ButtonPress(string s){
        if(!done){
            int value = int.Parse(s);
            Debug.Log(value);
            if(code[correct] == value){
                Debug.Log("OK" + correct.ToString());
                correct = correct + 1;
                UpdateScreen();
            }
            else{
                Fail();
            }
            if(correct >= 4){
                Succes();
            }
        }
    }

    void UpdateScreen(){
        string screen = ("screen"+correct.ToString());
        GameObject obj = GameObject.Find(screen);
        ChangeColor(obj, Color.green);
    }

    void Fail(){
        correct = 0;
        for (int i = 0; i < code.Length; i++){
            string screen = ("screen"+code[i].ToString());
            GameObject obj = GameObject.Find(screen);
            ChangeColor(obj, Color.red);
        }
        Debug.Log("False");
    }

    void Succes(){
        print("Succes");
        done = true;
    }

    // GameObject screen1;
    // GameObject screen3;
    // GameObject screen6;
    // public int progressint;
    // int [] array_sequence = new int[3] {1,3,6};
    // Start is called before the first frame update


    // // Update is called once per frame
    // void Update()
    // {
    //     for (int i = 0; i < array_sequence.Length; i++){
    //         string screennumber = ("screen"+array_sequence[i].ToString());
    //         Debug.Log(screennumber);
    //         GameObject obj = GameObject.Find(screennumber);
    //         ChangeColor(obj, Color.blue);
    //     }
    // }
}
