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

    void Start()
    {
        one = GameObject.Find("1");       
        two = GameObject.Find("2");       
        three = GameObject.Find("3");       
        four = GameObject.Find("4");       
        five = GameObject.Find("5");
        state = new bool[6] {false, false, false, false, false, false};    
        done = false; 
    }

    //Flips the switch given as input.
    private void flipTheFlipper(GameObject flipper){
        int flipperi = int.Parse(flipper.name);
        print(flipperi);
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
        if(!done){
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
        }
        else{
            Succes();
        }
    }
    public void Succes(){
        //Succes
    }
}
