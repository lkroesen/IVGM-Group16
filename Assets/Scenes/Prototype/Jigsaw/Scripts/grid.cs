using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public bool[] positioned;
    private bool check_win = true;

    // Start is called before the first frame update
    void Start()
    {
        positioned = new bool[16];

        for(int i=0; i<16; i++){
            positioned[i]=false;
        }
        positioned[7]=true;
        positioned[3]=true;
    }

    // Update is called once per frame
    void Update()
    {
      check_win = true;
      for(int i=0; i<16; i++){
          if(positioned[i]==false){
            check_win = false;
          }
      }

      if(check_win==true){
        //insert code here
        print("won");
      }
    }
}
