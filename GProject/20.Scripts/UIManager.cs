using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainPannel;
    public GameObject characterPannel;
    public GameObject recordPannel;
    public GameObject exitPannel;


    public void CharacterPannel()
    {
        mainPannel.SetActive(false);
        characterPannel.SetActive(true);
    }

    public void WarriorSelect()
    {
        SceneManager.LoadScene(0);
    }
    public void ArchorSelect()
    {
        SceneManager.LoadScene(1);
    }
}
