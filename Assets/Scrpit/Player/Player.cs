using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;


public class Player : MonoBehaviour
{
    private bool isUnitSelected = false;
    public Vector2 targetPosition;
    private float moveSpeed = 15.0f;
    private float attackRange = 5f;
    private float AtkDelay;

    int Damage = 0;
    int FullDamage = 0;


    [SerializeField]
    private bool MoveCheck = false;
    private bool Move = false;

    [SerializeField]
    private GameObject SelectPrefab;
    GameObject prefab;

    [SerializeField]
    PlayerState playerstate;

    PhotonView photonView;

    Animator animator;
    Vector2 previousPos;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        prefab = Instantiate(SelectPrefab,gameObject.transform.position,Quaternion.identity);
        prefab.SetActive(false);
        playerstate = FindObjectOfType<PlayerState>();
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        previousPos = gameObject.transform.position;
        MoveCheck = false;
        Move = false;
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            PlayerMove();
            EnemyCheck();
            BossCheck();
        }



        //남은 수랑 골드체크
        if (Input.GetKeyDown(KeyCode.X))
        {
            checkck();
            GameManager.Instance.Jewel += 200;
            Debug.Log(GameManager.Instance.Jewel);

        }
    }

    void OnDestroy()
    {
        Destroy(prefab);
    }

    void PlayerMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //레이캐스트를 카메라에서 월드로 쏨
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject) //해당 스크립트가 있는 오브젝트와 동일할 때
                {
                    if(photonView.IsMine)
                    {
                        isUnitSelected = true;
                        prefab.SetActive(true);
                    }
                }
                else //해당 오브젝트가 아니거나 null이면
                {
                    isUnitSelected = false;
                    prefab.SetActive(false);
                }
            }
        }

        if (!isUnitSelected && Input.GetMouseButtonDown(0))
        {
            isUnitSelected = false;
        }

        //해당 위치로 이동
        if (isUnitSelected && Input.GetMouseButtonDown(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Move = true;
            
        }

        if (Vector2.Distance((Vector2)transform.position, targetPosition) > 0.1f && Move)
        {
            MoveCheck = true;

            Vector2 currentPos = gameObject.transform.position;

            if (currentPos.x > previousPos.x)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            previousPos = currentPos;
            animator.SetBool("Run", true);
            if (!soundManager.audioSource.isPlaying)
            {
                soundManager.sound1();
                soundManager.audioSource.Play();
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            prefab.transform.position = gameObject.transform.position;
        }
        else
        {
            MoveCheck = false;
            Move = false;
            animator.SetBool("Run", false);
        }

    }

    

    void EnemyCheck()
    {
        AtkDelay += Time.deltaTime;
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (!MoveCheck && AtkDelay >= playerstate.atkSpeed)
        {
            GameObject nearestMonster = null;
            float nearestDistance = Mathf.Infinity;
            Vector3 playerPos = transform.position;

            foreach (GameObject monster in monsters)
            {
                float distance = Vector3.Distance(playerPos, monster.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestMonster = monster;
                }
            }

            if (nearestMonster != null && nearestDistance <= attackRange)
            {
                dam(nearestMonster);
                animator.SetTrigger("Attack");
                soundManager.sound0();
                soundManager.audioSource.Play();
                AtkDelay = 0;
            }
        }
    }

    void BossCheck()
    {
        AtkDelay += Time.deltaTime;
        GameObject Boss = GameObject.FindWithTag("Boss");

        if (!MoveCheck && AtkDelay >= playerstate.atkSpeed && Boss != null)
        {
            
            GameObject nearestMonster = null;
            float nearestDistance = Mathf.Infinity;
            Vector3 playerPos = transform.position;

            float distance = Vector3.Distance(playerPos, Boss.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestMonster = Boss;
            }

            if (nearestMonster != null && nearestDistance <= attackRange)
            {
                Dam(nearestMonster);
                animator.SetTrigger("Attack");
                soundManager.sound0();
                soundManager.audioSource.Play();
                AtkDelay = 0;
            }
        }
    }


    void dam(GameObject target)
    {
        EnemyHP Enemy = target.GetComponent<EnemyHP>();

        if (Enemy != null )
        {
            float randomChance = Random.Range(0f, 1f);
            if (randomChance <= playerstate.criticalchance)
            {
               Damage = (int)((playerstate.dmg + (int)(playerstate.dmg * playerstate.Criticaldmg)));
            }
            else
            {
                Damage = playerstate.dmg;
            }

            FullDamage = (int)(Damage + Damage * playerstate.allDamage);

            Enemy.TakeDamage((int)(FullDamage));
            Debug.Log(FullDamage);
            if (Enemy.HP <= 0)
            {
                GameManager.Instance.remainEnemy -= 1;
                playerstate.gold += 30 * GameManager.Instance.Round;
                UIManager.Instance.GoldUpdate();
            }
        }
    }

    void Dam(GameObject target)
    {
        Boss boss = target.GetComponent<Boss>();

        if (boss != null)
        {
            float randomChance = Random.Range(0f, 1f);
            if (randomChance <= playerstate.criticalchance)
            {
                Damage = (int)((playerstate.dmg + (int)(playerstate.dmg * playerstate.Criticaldmg)));
            }
            else
            {
                Damage = playerstate.dmg;
            }

            FullDamage = (int)(Damage + Damage * playerstate.allDamage);

            boss.TakeDamage((int)(FullDamage));
            Debug.Log(FullDamage);
        }
    }



    void checkck()
    {
        Debug.Log("골드 : " + playerstate.gold);
    }
}
