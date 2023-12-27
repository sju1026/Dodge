using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BS : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;
    public float traceRate = 10f;

    public bool targeting = false;
    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (targeting == true)
        {
            if (timeAfterSpawn >= spawnRate)
            {
                timeAfterSpawn = 0f;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.LookAt(target);

                spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            }
        }
        timeAfterSpawn += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targeting = true;
            Debug.Log("Target Find");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targeting = false;
            Debug.Log("Target Lost");
        }
    }
}