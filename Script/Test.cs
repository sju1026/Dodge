using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string characterName = "�̹�";
        char bloodTye = 'A';
        int age = 20;
        float height = 168.8f;
        bool isFemale = true;
         
        Debug.Log("ĳ���� �̸� : " + characterName);
        Debug.Log("������ : " + bloodTye);
        Debug.Log("���� : " + age);
        Debug.Log("Ű : " + height);
        Debug.Log("�����ΰ�? : " + isFemale);

    }
}
