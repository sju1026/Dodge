using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform Taget;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            col.transform.position = Taget.position;
        }
    }

}
