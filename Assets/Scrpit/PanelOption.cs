using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOption : MonoBehaviourPunCallbacks
{
    
    public GameObject Chatpanel;
    public GameObject MainPanel;
    public GameObject Inventory;
    public GameObject Option;


    void Start()
    {
        Chatpanel.SetActive(false);
        MainPanel.SetActive(false);
        Option.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Panelset();

    }

    void Panelset()
    {
        if (PhotonNetwork.InRoom)
        {
            MainPanel.SetActive(true);
        }
        else
        {
            MainPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.T) && PhotonNetwork.InRoom)
        {
            if (Chatpanel.activeSelf)
            {
                Chatpanel.SetActive(false);
            }
            else
            {
                Chatpanel.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.O) && PhotonNetwork.InRoom)
        {
            if (Option.activeSelf)
            {
                Option.SetActive(false);
            }
            else
            {
                Option.SetActive(true);
            }
        }


        if (PhotonNetwork.InRoom || PhotonNetwork.InLobby)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (Inventory.activeSelf)
                {
                    Inventory.SetActive(false);
                }
                else
                {
                    Inventory.SetActive(true);
                }

            }
        }
    }

    public void OptionPanel()
    {
        if(Option.activeSelf)
        {
            Option.SetActive(false);
        }
        else
        {
            Option.SetActive(true) ;
        }
    }

}
