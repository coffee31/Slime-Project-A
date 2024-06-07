using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject[] PreFab;
    public bool BossON;
    [SerializeField] float BossCreate;
    [SerializeField] int index;

    private string prefabPath;

    private GameObject mainBoss;

    private int currentBossHP;

    public GameObject text;

    private void Start()
    {
        prefabPath = "BossPrefab/" + PreFab[index].name;
    }



    void Update()
    {
        if (text == null)
            text = GameObject.FindWithTag("TimerText");

        if (text != null)
        {
            photonView.RPC("Textupdate", RpcTarget.All);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            spawnStart();
            photonView.RPC("SyncVariablesRPC", RpcTarget.OthersBuffered, BossON, BossCreate, index, currentBossHP);
        }
    }

    public void spawnStart()
    {
        if (!BossON)
        {
            BossCreate -= Time.deltaTime;
        }

        if (BossCreate <= 0)
        {
            BossON = true;
            BossCreate = 120;

            prefabPath = "BossPrefab/" + PreFab[index].name;

            mainBoss = PhotonNetwork.Instantiate(prefabPath, transform.position, Quaternion.identity);
            mainBoss.name = "SlimeBoss";
            if(index < 4)
                index++;

        }
    }
    [PunRPC]
    void Textupdate()
    {
         text.GetComponent<Text>().text = "Boss Timer : " + (int)BossCreate;
    }

    [PunRPC]
    void SyncVariablesRPC(bool bossOn, float bossCreate, int bossIndex)
    {
        BossON = bossOn;
        BossCreate = bossCreate;
        index = bossIndex;
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);

        if (BossON)
        {
            prefabPath = "BossPrefab/" + PreFab[index - 1].name;

            mainBoss = PhotonNetwork.Instantiate(prefabPath, transform.position, Quaternion.identity);
            mainBoss.name = "resetBoss";
        }
    }
}