using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //변경해야할거 State 원상복귀
    // UI로 업글한거 원상복귀
    // Round랑 Start버튼 누른 상태 원상복귀


    private static PlayerState _instance = null;
    public static PlayerState Instance => _instance;


    [Header("플레이어 스탯")]
    private int DMG;
    public int dmg
    {
        get { return DMG; }
        set { DMG = value; }
    }

    private float AtkSpeed;
    public float atkSpeed
    {
        get { return AtkSpeed; }
        set { AtkSpeed = value; }
    }

    private float CriticalDMG;
    public float Criticaldmg
    {
        get { return CriticalDMG; }
        set { CriticalDMG = value; }
    }

    private float CriticalChance;
    public float criticalchance
    {
        get { return CriticalChance; }
        set { CriticalChance = value; }
    }

    private float MagicDMG;
    public float magicdmg
    {
        get { return MagicDMG; }
        set { MagicDMG = value; }
    }

    private float AllDamage;
    public float allDamage
    {
        get { return AllDamage; }
        set {  AllDamage = value; }
    }

    private int Gold;
    public int gold
    {
        get { return Gold; }
        set { Gold = value; }
    }

    void Awake()
    {
        PlayerSet();

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void PlayerSet()
    {
        DMG = 40; //5씩 상승 [15가 초기값]
        AtkSpeed = 1.5f; // -0.001씩해서 스탯으로 1초로 만들고 템으로 -0.3까지 내릴 수 있게함
        CriticalDMG = 0.50f; // 기본배수 50% 추가딜 -> 0.01단위로 상승
        CriticalChance = 0.05f; //기본 5퍼센트 확률 1% 상승 [최대 100%] -> 넘어가면 크뎀에 추가데미지로 전환
        MagicDMG = 5; // 5씩 상승
        AllDamage = 0.03f; //3%씩 상승
        gold = 0;
    }

}
