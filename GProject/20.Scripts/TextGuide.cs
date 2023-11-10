using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGuide : MonoBehaviour
{ 
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.jumpMissionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.jumpMissionText.SetActive(false);
        }
    }
}
