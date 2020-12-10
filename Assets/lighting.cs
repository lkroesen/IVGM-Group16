using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighting : MonoBehaviour
{
    public static GameObject[] allChildren;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.childCount);
        int i = 0;

        allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

    }

    public void setAllLighting(float intensity){
        foreach (GameObject child in allChildren)
        {
            child.GetComponent<Light>().intensity = intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
