using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    private static GameManager m_instance;

    [Header("Game Setting")]
    public GameObject menuCam;
    public GameObject gameCam;
    public PlayerM player;
    public Weapons weapon;

    public GameObject menuPannel;
    public GameObject gamePannel;
    public GameObject overPannel;

    bool isStart = false;


    [Header("Player State")]
    public float playTime;
    public Text playTimeTxt;

    public Text damageTxt;
    public Text speedTxt;
    public Text depenceTxt;
    public Text attackRateTxt;

    public Text playerHealthTxt;
    public Text playerMpTxt;
    public RectTransform playerHealthBar;
    public RectTransform playerMPBar;

    [Header("Mission Gate")]
    public GameObject other1Door;
    public GameObject other2Door;
    public GameObject other3Door;
    public GameObject[] stage1Monster;
    public GameObject[] stage2Monster;
    public GameObject[] stage3Monster;
    public GameObject jumpMissionText;

    [Header("Stage Gate")]
    public GameObject[] stage1;
    public GameObject[] stage2;
    public GameObject[] stage3;
    public GameObject[] stage4;
    public GameObject[] stage5;
    public GameObject[] stage6;
    public GameObject[] stage7;
    public GameObject[] stage8;
    public GameObject[] stage9;
    public GameObject[] stage10;
    public GameObject[] stage11;
    public GameObject[] stage12;
    public GameObject[] stage13;

    /*[Header("Stage Boolean")]
    bool stageClear_1 = false;
    bool stageClear_2 = false;
    bool stageClear_3 = false;
    bool stageClear_4 = false;
    bool stageClear_5 = false;
    bool stageClear_6 = false;
    bool stageClear_7 = false;
    bool stageClear_8 = false;
    bool stageClear_9 = false;
    bool stageClear_10 = false;
    bool stageClear_11 = false;
    bool stageClear_12 = false;
    bool stageClear_13 = false;*/

    void Awake()
    {
        player = FindObjectOfType<PlayerM>();
        weapon = FindObjectOfType<Weapons>();
    }

    private void Start()
    {
        isStart = true;
    }

    void Update()
    {
        if (isStart)
        {
            playTime += Time.deltaTime;
        }

        if (stage1Monster.Length == 0)
        {
            other1Door.SetActive(true);
        }
        else if (stage2Monster.Length == 0)
        {
            other2Door.SetActive(true);
        }
        else if (stage3Monster.Length == 0)
        {
            other3Door.SetActive(true);
        }
    }

    void LateUpdate()
    {
        // Timer
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int sec = (int)(playTime % 60);
        playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);

        // Player State
        playerHealthTxt.text = player.health + " / " + player.maxHealth;
        playerMpTxt.text = player.mp + " / " + player.maxMP;

        playerHealthBar.localScale = new Vector3((float)player.health / player.maxHealth, 1, 1);
        playerMPBar.localScale = new Vector3((float)player.mp / player.maxMP, 1, 1);


        damageTxt.text = weapon.damage.ToString();
        speedTxt.text = player.speed.ToString();
        depenceTxt.text = player.defence.ToString();
        attackRateTxt.text = weapon.rate.ToString();

        // CheckMonster();

    }

    /*public void CheckMonster()
    {
        int testsum = 0;

        for (int i = 0; i < stage1.Length; i++)
        {
            if (stage1[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage1.Length) {
                stageClear_1 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage2.Length; i++)
        {
            if (stage2[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage2.Length)
            {
                stageClear_2 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage3.Length; i++)
        {
            if (stage3[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage3.Length)
            {
                stageClear_3 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage4.Length; i++)
        {
            if (stage4[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage4.Length)
            {
                stageClear_4 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage5.Length; i++)
        {
            if (stage5[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage5.Length)
            {
                stageClear_5 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage6.Length; i++)
        {
            if (stage6[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage6.Length)
            {
                stageClear_6 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage7.Length; i++)
        {
            if (stage7[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage7.Length)
            {
                stageClear_7 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage8.Length; i++)
        {
            if (stage8[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage8.Length)
            {
                stageClear_8 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage9.Length; i++)
        {
            if (stage9[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage9.Length)
            {
                stageClear_9 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage10.Length; i++)
        {
            if (stage10[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage10.Length)
            {
                stageClear_10 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage11.Length; i++)
        {
            if (stage11[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage11.Length)
            {
                stageClear_11 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage12.Length; i++)
        {
            if (stage12[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage12.Length)
            {
                stageClear_12 = true;
                testsum = 0;
            }
        }

        for (int i = 0; i < stage13.Length; i++)
        {
            if (stage13[i] == null)
            {
                testsum += 1;
            }
            else
            {
                testsum += 0;
            }

            if (testsum == stage13.Length)
            {
                stageClear_13 = true;
                testsum = 0;
            }
        }
    }*/

}
