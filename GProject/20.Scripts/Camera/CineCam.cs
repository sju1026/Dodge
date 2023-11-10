using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineCam : MonoBehaviour
{
    public CinemachineVirtualCamera virtualcam;
    // Start is called before the first frame update
    void Awake()
    {
        virtualcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        virtualcam.Follow = FindObjectOfType<PlayerM>().transform;
        virtualcam.LookAt = FindObjectOfType<PlayerM>().transform;
    }

}
