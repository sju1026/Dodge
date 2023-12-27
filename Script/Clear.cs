using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public PlayerController pc;
    public bool isGameClear = false; // ���� ���� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.OnGameClear();
            other.gameObject.SetActive(false);
        }
    }
}
