using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{

    public string leftOrRight="right";
    public bool isOpen = false;

    private int ix = 0;
    private string _LeftOrRight
    {
        get { return leftOrRight; }
        set
        {
            string strung = value.ToLower();
            if (strung != "left" && strung != "right" && strung != "r" && strung != "l")
            {
                throw new Exception("Left Or Right must have a value that is either 'Left' or 'Right'.");
            }
            if (strung == "l") { strung = "left"; }
            if (strung == "r") { strung = "right"; }
            leftOrRight = strung;
        }
    }

    public bool _IsOpen
    {
        get { return isOpen; }
        set { 
            if (value) { DoorOpens();  } 
            isOpen= value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _IsOpen = _IsOpen;
        
    }

    void DoorOpens()
    {
        switch (_LeftOrRight)
        {
            case "left":
                transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case "right":
                transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
        }
    }
}
