using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical";
    public string rotateAxisName = "Horizontal";
    //public string fireButtonName = "Fire1";
    //public string reloadButtonName = "Reload";

    public float move { get; private set; }
    public float rotate { get; private set; }

    void Update()
    {
        move = 0;
        rotate = 0;

        move = Input.GetAxis(moveAxisName);
        rotate = Input.GetAxis(rotateAxisName);
    }
}
