using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject[] normalItem;
    public GameObject[] rareItem;
    public GameObject[] epicItem;
    public Transform[] spawnPoint;

    BoxCollider boxCollider;

    public PlayerM player;
    public int boxValue;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void RandomSpawn()
    {
        if(boxValue == 1)
        {
            int index = Random.Range(0, normalItem.Length);
            int transform = Random.Range(0, spawnPoint.Length);
            Instantiate(normalItem[index], spawnPoint[transform].transform.position, spawnPoint[transform].transform.rotation);
        }
        else if (boxValue == 2)
        {
            int index = Random.Range(0, rareItem.Length);
            int transform = Random.Range(0, spawnPoint.Length);
            Instantiate(rareItem[index], spawnPoint[transform].transform.position, spawnPoint[transform].transform.rotation);
        }
        else if (boxValue == 3)
        {
            int index = Random.Range(0, normalItem.Length);
            int transform = Random.Range(0, spawnPoint.Length);
            int luck = Random.Range(0, 5);
            if(luck == 0)
            {
                Instantiate(epicItem[index], spawnPoint[transform].transform.position, spawnPoint[transform].transform.rotation);
            }
            else
            {
                Instantiate(rareItem[index], spawnPoint[transform].transform.position, spawnPoint[transform].transform.rotation);
            }
        }
        else if (boxValue == 4)
        {
            int index = Random.Range(0, epicItem.Length);
            int transform = Random.Range(0, spawnPoint.Length);
            Instantiate(epicItem[index], spawnPoint[transform].transform.position, spawnPoint[transform].transform.rotation);
        }

        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (player.iDown)
            {
                Debug.Log("PlayerInteraction");
                RandomSpawn();
            }
        }
    }
}
