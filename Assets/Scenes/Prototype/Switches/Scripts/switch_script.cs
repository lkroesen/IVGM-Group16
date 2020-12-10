using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_script : MonoBehaviour
{
    // Start is called before the first frame update
    static private bool [] state;
    static private GameObject one;
    static private GameObject two;
    static private GameObject three;
    static private GameObject four;
    static private GameObject five;

    private bool done;
    private bool active;

    public GameObject light1;
    public GameObject light2;

    void Start()
    {
        one = GameObject.Find("1");       
        two = GameObject.Find("2");       
        three = GameObject.Find("3");       
        four = GameObject.Find("4");       
        five = GameObject.Find("5");
        state = new bool[6] {true, false, false, false, false, false};    
        done = false;
        activate();
    }

    void activate(){
        one.transform.RotateAround(one.transform.parent.position, Vector3.forward, -90.0f);
        two.transform.RotateAround(two.transform.parent.position, Vector3.forward, -90.0f);
        three.transform.RotateAround(three.transform.parent.position, Vector3.forward, -90.0f);
        four.transform.RotateAround(four.transform.parent.position, Vector3.forward, -90.0f);
        five.transform.RotateAround(five.transform.parent.position, Vector3.forward, -90.0f);
        active = true;
    }

    //Flips the switch given as input.
    private void flipTheFlipper(GameObject flipper){
        int flipperi = int.Parse(flipper.name);
        if(state[flipperi]){
            flipper.transform.RotateAround(flipper.transform.parent.position, Vector3.forward, -90.0f);
            state[flipperi] = false;
        }
        else
        {
            flipper.transform.RotateAround(flipper.transform.parent.position, Vector3.forward, 90.0f);
            state[flipperi] = true;
        }
    }

    //Determines which switches should flip given the input.
    public void flip(GameObject flipper){
        if(!done && active){
            switch (flipper.name)
            {
                case "1":
                    flipTheFlipper(one);
                    flipTheFlipper(five);
                    flipTheFlipper(four);
                    break; 
                case "2":
                    flipTheFlipper(one);
                    flipTheFlipper(two);
                    flipTheFlipper(three);
                    break; 
                case "3":
                    flipTheFlipper(one);
                    flipTheFlipper(three);
                    flipTheFlipper(five);
                    break; 
                case "4":
                    flipTheFlipper(two);
                    flipTheFlipper(three);
                    flipTheFlipper(four);
                    break; 
                case "5":
                    flipTheFlipper(two);
                    flipTheFlipper(three);
                    flipTheFlipper(five);
                    break; 
                default:
                    break;
            }
            print(state[1].ToString() + state[2].ToString() + state[3].ToString() + state[4].ToString() + state[5].ToString());
            bool allTrue = true;
            foreach (bool s in state){
                if(s == false){
                    allTrue = false;
                }
            }
            if(allTrue == true){
                print("Allgood");
                Succes();
            }
        }

    }
    public void Succes(){
        done = true;
        active = false;
        light1.GetComponent<Light>().intensity = 1f;
        light2.GetComponent<Light>().intensity = 1f;
    }
}
