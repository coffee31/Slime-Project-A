using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int Round;
    public int remainEnemy = 0;
    public int Jewel = 0;
    public int BossCount = 1;
    public bool startCheck = false;
    public bool btnON = false;


    private void Awake()
    {
        Round = 1;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
