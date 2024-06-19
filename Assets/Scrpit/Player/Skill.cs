using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public bool SkillA;
    public bool SkillB;
    public bool SkillC;
    public bool SkillD;
    public bool SkillE;

    private void Start()
    {
        SkillA = true;
        SkillB = true;
        SkillC = true;
        SkillD = true;
        SkillE = true;
    }


    public void SkillON()
    {
        if (Random.Range(0f, 1f) <= 0.2f && SkillA)
        {
            SkillAON();
        }

        if (Random.Range(0f, 1f) <= 0.15f && SkillB)
        {
            SkillBON();
        }

        if (Random.Range(0f, 1f) <= 0.1f && SkillC)
        {
            SkillCON();
        }

        if (Random.Range(0f, 1f) <= 0.05f && SkillD)
        {
            SkillDON();
        }

        if (Random.Range(0f, 1f) <= 0.01f && SkillE)
        {
            SkillEON();
        }
    }



    void SkillAON()
    {
        Debug.Log("20�� Ȯ���� ���ϴ�� ���� magic * 3��");

    }

    void SkillBON()
    {
        Debug.Log("15�� Ȯ���� ���� ��� ���� magic * 2.5��");
    }

    void SkillCON()
    {
        Debug.Log("10�� Ȯ���� ���� ���� 2��� [5�ʵ���]");
    }

    void SkillDON()
    {
        Debug.Log("5�� Ȯ���� ��ü ��� ���� magic * 2��");
    }

    void SkillEON()
    {
        Debug.Log("1�� Ȯ���� ��ü ��ų Ȯ�� 2��");
    }


}
