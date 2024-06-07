using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using ExitGames.Client.Photon; // ExitGames.Client.Photon.Hashtable ���

public class Boss : MonoBehaviourPunCallbacks, IPunObservable
{
    public int MaxHP;
    public int HP;
    public int DEF;
    public int Diamond;
    public int BossCount;

    HealthBar hpbar;
    public GameObject hpBarPrefab;
    GameObject hpbarinstance;

    public Canvas canvas;

    BossSpawn bossSpawn;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            hpbarinstance = Instantiate(hpBarPrefab, canvas.transform);
            if (hpbarinstance != null)
            {
                hpbar = hpbarinstance.GetComponent<HealthBar>();
                if (hpbar == null)
                {
                    Debug.LogError("HealthBar�� ã�� �� �����ϴ�.");
                }
            }
            else
            {
                Debug.LogError("hpBarPrefab�� ������ �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("Canvas�� ã�� �� �����ϴ�.");
        }

        HP = MaxHP;
    }

    private void LateUpdate()
    {
        if (hpbar != null && hpbarinstance != null)
        {
            hpbar.HPbarValue(HP, MaxHP);
            hpbarinstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 3, 0));
            hpbarinstance.transform.rotation = Quaternion.identity;
        }
        else
        {
            if (hpbar == null)
            {
                Debug.LogWarning("hpbar�� �����ϴ�.");
            }
            if (hpbarinstance == null)
            {
                Debug.LogWarning("hpbarinstance�� �����ϴ�.");
            }
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (damage > DEF)
        {
            HP -= (damage - DEF);
            Debug.Log("������");

            if (hpbar != null)
            {
                hpbar.HPbarValue(HP, MaxHP);
            }
            else
            {
                Debug.LogWarning("hpbar�� �����ϴ�.");
            }
        }

        HP = Mathf.Clamp(HP, 0, MaxHP);

        if (HP <= 0)
        {
            bossSpawn = FindObjectOfType<BossSpawn>();
            if (bossSpawn != null)
            {
                bossSpawn.BossON = false;
            }
            else
            {
                Debug.LogError("BossSpawn�� �����ϴ�.");
            }

            photonView.RPC("DistributeRewards", RpcTarget.All, Diamond, BossCount);
            photonView.RPC("DestroyBoss", RpcTarget.AllBuffered);
        }

        photonView.RPC("SyncBossHealth", RpcTarget.All, HP);
    }

    [PunRPC]
    void SyncBossHealth(int currentHP)
    {
        HP = currentHP;
        if (hpbar != null)
        {
            hpbar.HPbarValue(HP, MaxHP);
        }
        else
        {
            Debug.LogWarning("hpbar�� �����ϴ�.");
        }
    }

    [PunRPC]
    void DestroyBoss()
    {
        Destroy(gameObject);
    }

    [PunRPC]
    void DistributeRewards(int diamond, int bossCount)
    {
        GameManager.Instance.Jewel += diamond;
        PlayerState.Instance.gold += bossCount;
        UIManager.Instance.GoldUpdate();
    }

    private void OnDestroy()
    {
        if (hpbarinstance != null)
        {
            Destroy(hpbarinstance);
        }
        else
        {
            Debug.LogWarning("hpbarinstance�� �����ϴ�.");
        }
    }

    public void DamageRPC(int damage)
    {
        photonView.RPC("TakeDamage", RpcTarget.All, damage);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(HP);
        }
        else
        {
            HP = (int)stream.ReceiveNext();
            if (hpbar != null)
            {
                hpbar.HPbarValue(HP, MaxHP);
            }
            else
            {
                Debug.LogWarning("hpbar�� �����ϴ�.");
            }
        }
    }
}