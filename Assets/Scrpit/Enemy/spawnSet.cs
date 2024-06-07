using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class spawnSet : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monster;
    [SerializeField]
    private float spanwTime;
    [SerializeField]
    private Transform[] waypoint;
    [SerializeField]
    private int monsterCount;

    int Timer;
    GameObject clone;
    


    void Awake()
    {
        Timer = 120;
        monsterCount = 40;
        GameManager.Instance.startCheck = false;
        GameManager.Instance.remainEnemy = 0;
    }

    public void Startbtn()
    {
        if(!GameManager.Instance.startCheck)
        {
            GameManager.Instance.startCheck = true;
            StartCoroutine("SpawnEnemy");
            Debug.Log("½ºÅ¸Æ®");
        }
        else
        {
            MonsterClear();
        }
    }

    void MonsterClear()
    {
        GameManager.Instance.startCheck = false;
        StopAllCoroutines();
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject obj in objectsWithTag)
        {
            EnemyHP enemyHP = obj.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(enemyHP.MAXHP);
            }
        }
        GameManager.Instance.remainEnemy = 0;
    }

    public void OnLeaveRoom()
    {
        GameManager.Instance.Round = 1;
        MonsterClear();
    }

    private IEnumerator SpawnEnemy()
    {
        if(GameManager.Instance.startCheck)
        {
            Timer = 120;
            monsterCount = 40;
            while (monsterCount != 0)
            {
                int index = Mathf.Min((GameManager.Instance.Round - 1) / 5, monster.Length - 1);
                clone = Instantiate(monster[index]);


                EnemySet enemy = clone.GetComponent<EnemySet>();
                enemy.Setup(waypoint);
                GameManager.Instance.remainEnemy += 1;
                monsterCount--;
                Timer--;
                yield return new WaitForSeconds(spanwTime);
            }

            if (monsterCount == 0)
            {
                StartCoroutine(RemainTimer());
            }
        }

    }

    private IEnumerator RemainTimer()
    {
        while (Timer > 0 && GameManager.Instance.startCheck && GameManager.Instance.remainEnemy != 0)
        {
            Debug.Log(Timer);
            Timer--;
            yield return new WaitForSeconds(1.0f);
        }

        if(Timer <= 0 && GameManager.Instance.remainEnemy > 0)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Monster");

            foreach (GameObject obj in objectsWithTag)
            {
                EnemyHP enemyHP = obj.GetComponent<EnemyHP>();
                if (enemyHP != null)
                {
                    enemyHP.TakeDamage(enemyHP.MAXHP);
                }
            }
            GameManager.Instance.remainEnemy = 0;
        }

        if (Timer >= 0 && GameManager.Instance.remainEnemy <= 0)
        {
            StartCoroutine(SpawnEnemy());
        }
    }
}

