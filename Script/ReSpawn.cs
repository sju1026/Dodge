using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{

    public GameObject player;
    void Start()
    {
        player = Instantiate(player);
        player.transform.position = transform.position;
    }
}
