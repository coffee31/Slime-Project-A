using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public PhotonView pv;

    [Header("Login")]
    public GameObject LoginPanel;
    public InputField NickName;

    [Header("Lobby")]
    public GameObject LobbyPanel;
    public InputField RoomName;
    public InputField EnterRoomName;
    public Text NickNameText;
    public Text LobbyInfo;
    public Button[] LobbyBtn;
    public Button Next;
    public Button Previous;

    [Header("Room")]
    public GameObject RoomPanel;
    public InputField chatinput;
    public Text RoomInfo;
    public Text[] ChatText;

    Player player;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, MaxPage, Multiple;
    private Color[] colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, new Color(1, 0.5f, 0), Color.gray, Color.cyan, Color.magenta };

    BossSpawn bossSpawn;
    spawnSet spawn;

    [SerializeField]
    GameObject grid;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
        
    }

    private void Update()
    {
        LobbyInfo.text = "�κ� �ο� : " + (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + " ������ : " + PhotonNetwork.CountOfPlayers;

        if (PhotonNetwork.InRoom)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sendmsg();
                chatinput.ActivateInputField();
            }
        }
    }

    public void connet()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        grid.SetActive(true);
        LoginPanel.SetActive(false);
        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        PhotonNetwork.LocalPlayer.NickName = NickName.text;
        NickNameText.text = PhotonNetwork.LocalPlayer.NickName + "�� ������";
        myList.Clear();
       // SetupLobbyButtons();
    }

    public void Disconnet()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        LoginPanel.SetActive(true);
        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }

    public void RoomCreate()
    {
        PhotonNetwork.CreateRoom(RoomName.text == "" ? "Room" + Random.Range(0, 100) : RoomName.text, new RoomOptions { MaxPlayers = 8 });
    }

    
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[Multiple + num].Name);
        MyListRenewal();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i]))
                    myList.Add(roomList[i]);
                else
                    myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1)
                myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }

    private void MyListRenewal()
    {
        MaxPage = (myList.Count % LobbyBtn.Length == 0) ? myList.Count / LobbyBtn.Length : myList.Count / LobbyBtn.Length + 1;

        Previous.interactable = (currentPage <= 1) ? false : true;
        Next.interactable = (currentPage >= MaxPage) ? false : true;

        Multiple = (currentPage - 1) * LobbyBtn.Length;
        for (int i = 0; i < LobbyBtn.Length; i++)
        {
            LobbyBtn[i].interactable = (Multiple + i < myList.Count) ? true : false;
            LobbyBtn[i].transform.GetChild(0).GetComponent<Text>().text = (Multiple + i < myList.Count) ? myList[Multiple + i].Name : "";
            LobbyBtn[i].transform.GetChild(1).GetComponent<Text>().text = (Multiple + i < myList.Count) ? myList[Multiple + i].PlayerCount + "/" + myList[Multiple + i].MaxPlayers : "";
        }
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(EnterRoomName.text == "" ? "Room" + Random.Range(0, 100) : EnterRoomName.text, new RoomOptions { MaxPlayers = 8 }, null);
    }

    public override void OnJoinedRoom()
    {
        LobbyPanel.SetActive(false);
        RoomRenewal();
        chatinput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";

        if (PhotonNetwork.IsConnectedAndReady)
            Playerspawn();

        if (PhotonNetwork.IsMasterClient && GameObject.Find("BossPoint") == null)
        {
            GameObject bossPointObject = PhotonNetwork.InstantiateRoomObject("BossPoint", transform.position, Quaternion.identity);
            bossPointObject.name = "BossPoint";
        }

        PlayerState.Instance.PlayerSet();
        UIManager.Instance.setting();
    }

    void Playerspawn()
    {
        Vector3 baseSpawnPosition = new Vector3(0, 0, 0);

        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        int playerIndex = System.Array.IndexOf(players, PhotonNetwork.LocalPlayer);

        Vector3 spawnPosition = baseSpawnPosition + new Vector3(Random.Range(-15f, 15f), Random.Range(-7f, 7f), 0);

        GameObject playerObject = PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
        player = playerObject.GetComponent<Player>();

        playerObject.name = "Player";

        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = colors[playerIndex % colors.Length];
        }

        if (PhotonNetwork.IsMasterClient && GameObject.Find("BossPoint") != null)
        {
            GameObject bossPoint = GameObject.Find("BossPoint");
            GameObject bossObject = PhotonNetwork.Instantiate("BossPrefab", bossPoint.transform.position, Quaternion.identity);
            bossObject.transform.SetParent(bossPoint.transform);
        }
    }

    private void RoomRenewal()
    {
        RoomInfo.text = "�� �̸� : " + PhotonNetwork.CurrentRoom.Name + " / " + "�ο� �� : " + PhotonNetwork.CurrentRoom.PlayerCount + "�� ";
    }

    public void sendmsg()
    {
        string msg = PhotonNetwork.NickName + " : " + chatinput.text;
        pv.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + chatinput.text);
        chatinput.text = "";
        chatinput.ActivateInputField();
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        spawn = FindObjectOfType<spawnSet>();
        spawn.OnLeaveRoom();
        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        grid.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");

        if (PhotonNetwork.IsMasterClient)
        {
            TransferOwnershipOfBossPoint();
        }
    }

    private void TransferOwnershipOfBossPoint()
    {
        GameObject bossPoint = GameObject.Find("BossPoint");
        if (bossPoint != null)
        {
            PhotonView photonView = bossPoint.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

                Photon.Realtime.Player newOwner = null;
                foreach (var player in players)
                {
                    if (player != PhotonNetwork.LocalPlayer)
                    {
                        newOwner = player;
                        break;
                    }
                }

                if (newOwner != null)
                {
                    photonView.TransferOwnership(newOwner);
                    Debug.Log("���ο� ������ : " + newOwner.NickName);
                }
                else
                {
                    Debug.Log("���� �����Ͱ� �����ϴ�.");
                }
            }
            else
            {
                Debug.Log("���� ����Ʈ�� �������� ������ �ƴմϴ�.");
            }
        }
        else
        {
            Debug.Log("���� ����Ʈ �����ϴ�.");
        }
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        TransferOwnershipOfBossPoint();

        if (PhotonNetwork.IsMasterClient && GameObject.Find("BossPoint") != null)
        {
            // ���� ��ȯ�� ���� �޼��� ȣ��
            bossSpawn.spawnStart();
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("�� ���� ����");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        RoomName.text = "";
        RoomCreate();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        RoomName.text = "";
        RoomCreate();
    }

    public void List()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("���� �� �ο� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.Log("���� �� �ִ��ο� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
                Debug.Log(playerStr);
            }
        }
        else
        {
            Debug.Log("���� �ο� : " + PhotonNetwork.CountOfPlayers);
            Debug.Log("�� ���� : " + PhotonNetwork.CountOfRooms);
            Debug.Log("��� �濡 �ִ� �ο� �� : " + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("���� ���� : " + PhotonNetwork.IsConnected);
        }
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
        {
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        }

        if (!isInput)
        {
            for (int i = 1; i < ChatText.Length; i++)
            {
                ChatText[i - 1].text = ChatText[i].text;
            }
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
}