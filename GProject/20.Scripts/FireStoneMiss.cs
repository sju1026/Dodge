using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStoneMiss : MonoBehaviour
{
    // public int stoneValue;
    public bool lightSet;
    //public GameObject[] sprites;
    public GameObject test1;
    public GameObject test2;

    private void Start()
    {
        lightSet = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" || other.tag == "Bullet") 
        {
            
            FireStoneMiss first = test1.GetComponentInParent<FireStoneMiss>();
            FireStoneMiss second = test2.GetComponentInParent<FireStoneMiss>();
            GameObject firstLight = test1;
            GameObject secondLight = test2;
            Debug.LogWarning("왜때려 야발");

            if (first.lightSet == true && second.lightSet == true || second.lightSet == true && first.lightSet == true)
            {
                Debug.LogWarning("전부 펄스");
                first.lightSet = false;
                second.lightSet = false;
                firstLight.gameObject.SetActive(false);
                secondLight.gameObject.SetActive(false);
            }
            else if (first.lightSet == true && second.lightSet == false || second.lightSet == false && first.lightSet == true)
            {
                Debug.LogWarning("하나 트루");
                first.lightSet = false;
                second.lightSet = true;
                firstLight.gameObject.SetActive(false);
                secondLight.gameObject.SetActive(true);
            }
            else if (first.lightSet == false && second.lightSet == true || second.lightSet == true && first.lightSet == false)
            {
                Debug.LogWarning("하나 트루");
                first.lightSet = true;
                second.lightSet = false;
                firstLight.gameObject.SetActive(true);
                secondLight.gameObject.SetActive(false);
            }
            else if (first.lightSet == false && second.lightSet == false || second.lightSet == false && first.lightSet == false)
            {
                Debug.LogWarning("전부 트루");
                first.lightSet = true;
                second.lightSet = true;
                firstLight.gameObject.SetActive(true);
                secondLight.gameObject.SetActive(true);
            }
        }
    }
}
