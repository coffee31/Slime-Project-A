using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //�����ؾ��Ұ� State ���󺹱�
    // UI�� �����Ѱ� ���󺹱�
    // Round�� Start��ư ���� ���� ���󺹱�


    private static PlayerState _instance = null;
    public static PlayerState Instance => _instance;


    [Header("�÷��̾� ����")]
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
        DMG = 40; //5�� ��� [15�� �ʱⰪ]
        AtkSpeed = 1.5f; // -0.001���ؼ� �������� 1�ʷ� ����� ������ -0.3���� ���� �� �ְ���
        CriticalDMG = 0.50f; // �⺻��� 50% �߰��� -> 0.01������ ���
        CriticalChance = 0.05f; //�⺻ 5�ۼ�Ʈ Ȯ�� 1% ��� [�ִ� 100%] -> �Ѿ�� ũ���� �߰��������� ��ȯ
        MagicDMG = 5; // 5�� ���
        AllDamage = 0.03f; //3%�� ���
        gold = 0;
    }

}
