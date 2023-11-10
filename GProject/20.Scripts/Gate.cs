using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject[] stageList;

    public PlayerM player;
    BoxCollider box;

    public float gateMoveValue;
    public int gateValue;
    public int testnum = 0;

    private void Start()
    {
        player = FindObjectOfType<PlayerM>();
        box = GetComponent<BoxCollider>();

        box.enabled = false;
        GateImport();
    }

    private void LateUpdate()
    {
        StageClear();
    }

    public void GateImport()
    {
        if (gateValue == 0)
        {
            stageList = GameManager.instance.stage1;
        }
        else if (gateValue == 1)
        {
            stageList = GameManager.instance.stage2;
        }
        else if (gateValue == 2)
        {
            stageList = GameManager.instance.stage3;
        }
        else if (gateValue == 3)
        {
            stageList = GameManager.instance.stage4;
        }
        else if (gateValue == 4)
        {
            stageList = GameManager.instance.stage5;
        }
        else if (gateValue == 5)
        {
            stageList = GameManager.instance.stage6;
        }
        else if (gateValue == 6)
        {
            stageList = GameManager.instance.stage7;
        }
        else if (gateValue == 7)
        {
            stageList = GameManager.instance.stage8;
        }
        else if (gateValue == 8)
        {
            stageList = GameManager.instance.stage9;
        }
        else if (gateValue == 9)
        {
            stageList = GameManager.instance.stage10;
        }
        else if (gateValue == 10)
        {
            stageList = GameManager.instance.stage11;
        }
        else if (gateValue == 11)
        {
            stageList = GameManager.instance.stage12;
        }
        else if (gateValue == 12)
        {
            stageList = GameManager.instance.stage13;
        }
    }

    public void GateClose()
    {
        // gameObject.SetActive(true);

        float x = transform.position.x;
        float y = transform.position.y - gateMoveValue;
        float z = transform.position.z;
        
        transform.position = new Vector3(x, y, z);

        box.enabled = false;
    }

    public void GateOpen()
    {
        // gameObject.SetActive(false);

        float x = transform.position.x;
        float y = transform.position.y + gateMoveValue;
        float z = transform.position.z;
        
        transform.position = new Vector3(x, y, z);

    }

    public void StageClear()
    {
        for (int i = 0; i < stageList.Length; i++){
            if (stageList[i] == null)
            {
                testnum++;
            }
            else
            {
                testnum += 0;
            }
        }
        if (testnum == stageList.Length)
        {
            box.enabled = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (box.enabled == true)
            {
                GateOpen();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && box.enabled == true)
        {
            GateClose();
        }
    }
}
