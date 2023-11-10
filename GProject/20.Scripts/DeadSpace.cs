using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpace : MonoBehaviour
{
    PlayerM player;
    void Start()
    {
        player = FindObjectOfType<PlayerM>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            player.isDead = true;
            player.health = 0;
        }
    }

}
