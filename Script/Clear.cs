using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public PlayerController pc;
    public bool isGameClear = false; // 게임 오버 상태

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.OnGameClear();
            other.gameObject.SetActive(false);
        }
    }
}
