using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;
    public float traceRate = 10f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;
    Transform targets = null;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

    }

    // Update is called once per frame
    void Update()
    {
        target = FindObjectOfType<PlayerController>().transform;
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;
            if (targets != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.LookAt(target);
            }

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    void UpdateTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, traceRate);
        if (cols.Length > 0)
        {
            for(int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Player")
                {
                    Debug.Log("Physics Enemy : Target found");
                    targets = cols[i].gameObject.transform;
                }
                else
                {
                    Debug.Log("Physics Enemy : Target lost");
                    targets = null;
                }
            }
        }
    }
}