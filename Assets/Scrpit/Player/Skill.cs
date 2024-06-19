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
        Debug.Log("20퍼 확률로 단일대상 현재 magic * 3배");

    }

    void SkillBON()
    {
        Debug.Log("15퍼 확률로 범위 대상 현재 magic * 2.5배");
    }

    void SkillCON()
    {
        Debug.Log("10퍼 확률로 현재 공속 2배속 [5초동안]");
    }

    void SkillDON()
    {
        Debug.Log("5퍼 확률로 전체 대상 현재 magic * 2배");
    }

    void SkillEON()
    {
        Debug.Log("1퍼 확률로 전체 스킬 확률 2배");
    }


}
