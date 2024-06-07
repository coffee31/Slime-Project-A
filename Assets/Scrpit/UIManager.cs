using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;
    public static UIManager Instance => _instance;


    [Header("PlayerState")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI RoundText;
    public TextMeshProUGUI AtkText;
    public TextMeshProUGUI AtkSpeedText;
    public TextMeshProUGUI CriDamText;
    public TextMeshProUGUI CriChanceText;
    public TextMeshProUGUI MagicDamText;
    public TextMeshProUGUI AllDMGText;

    [Header("PanelText")]
    public TextMeshProUGUI AtkPanel;
    public TextMeshProUGUI AtkSpeedPanel;
    public TextMeshProUGUI CriDamPanel;
    public TextMeshProUGUI CriChancePanel;
    public TextMeshProUGUI MagicDamPanel;
    public TextMeshProUGUI AllDMGPanel;


    [SerializeField]
    private GameObject UpdatePanel;
    bool openPanel = false;

    [SerializeField]
    private GameObject ChatPanel;
    bool textpanel = false;

    PlayerState playerstate;

    int currentATK;
    int currentATKCost;

    float currentSpeed;
    int currentSpeedCost;

    float currentCriD;
    int currentCriDCost;

    float currentCriC;
    int currentCriCCost;

    int currentMagic;
    int currentMagicCost;


    public delegate void UpdateUI();
    UpdateUI updateUI;



    private void Awake()
    {
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


    void Start()
    {
        setting();
    }

    public void setting()
    {
        playerstate = PlayerState.Instance;
        init();
        updateUI += GoldUpdate;
        updateUI += TextUpdate;
        updateUI += RoundUpdate;
        updateUI += UPPanel;
        updateUI();

    }


    private void init()
    {
        currentATK = 0;
        currentATKCost = 100;

        currentSpeed = 0;
        currentSpeedCost = 100;

        currentCriD = 0;
        currentCriDCost = 100;

        currentCriC = 0;
        currentCriCCost = 100;

        currentMagic = 0;
        currentMagicCost = 100;

    }


    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.U))
        {
            if(!openPanel)
            {
                openPanel = true;
                UpdatePanel.SetActive(openPanel);
            }
            else
            {
                openPanel = false;
                UpdatePanel.SetActive(openPanel);
            } 
        }
    }

    public void TextUI()
    {
        if (!textpanel)
        {
            textpanel = true;
            ChatPanel.SetActive(textpanel);
        }
        else
        {
            textpanel = false;
            ChatPanel.SetActive(textpanel);
        }
    }

    public void UpgradeUI()
    {
        if (!openPanel)
        {
            openPanel = true;
            UpdatePanel.SetActive(openPanel);
        }
        else
        {
            openPanel = false;
            UpdatePanel.SetActive(openPanel);
        }
    }

    public void GoldUpdate()
    {
        if(goldText != null)
        {
            goldText.text = "Gold : " + playerstate.gold.ToString();
        }
    }
    public void TextUpdate()
    {
        if (AtkText != null)
        {
            AtkText.text = "Attack DMG   : " + playerstate.dmg.ToString();
        }
        if (AtkSpeedText != null)
        {
            AtkSpeedText.text = "Attack Speed : " + playerstate.atkSpeed.ToString("F2");
        }
        if (CriDamText != null)
        {
            CriDamText.text = "Critical DMG : " + playerstate.Criticaldmg.ToString("F2");
        }
        if (CriChanceText != null)
        {
            CriChanceText.text = "Critical Chance  : " + playerstate.criticalchance.ToString("F2");
        }
        if (MagicDamText != null)
        {
            MagicDamText.text = "Magic DMG : " + playerstate.magicdmg.ToString();
        }
        if (AllDMGText != null)
        {
            AllDMGText.text = "All DMG      : " + playerstate.allDamage.ToString("F2");
        }
    }

    public void RoundUpdate()
    {
        if (RoundText != null)
        {
            RoundText.text = "Round : " + GameManager.Instance.Round.ToString();
        }
    }

    public void UPPanel()
    {
        if (AtkPanel != null)
        {
            AtkPanel.text = "Attack DMG : UP : 5 / " + "Current UP : " + currentATK + " / Cost : " + currentATKCost;
        }
        if (AtkSpeedPanel != null)
        {
            AtkSpeedPanel.text = "Attack Speed : UP : -0.001% / " + "Current UP : " + currentSpeed.ToString("F3") + " / Cost : " + currentSpeedCost;
        }
        if (CriDamPanel != null)
        {
            CriDamPanel.text = "Critical DMG : UP : 0.01% / " + "Current UP : " + currentCriD.ToString("F2") + " / Cost : " + currentCriDCost;
        }
        if (CriChancePanel != null)
        {
            CriChancePanel.text = "Critical Chance : UP : 0.01% / " + "Current UP : " + currentCriC.ToString("F2") + " / Cost : " + currentCriCCost;
        }
        if (MagicDamPanel != null)
        {
            MagicDamPanel.text = "Magic DMG : UP : 5 / " + "Current UP : " + currentMagic.ToString() + " / Cost : " + currentMagicCost;
        }
    }


    public void RoundUP()
    {
        if(!GameManager.Instance.startCheck)
        {
            GameManager.Instance.Round += 1;
            RoundUpdate();
        }

    }

    public void RoundDown()
    {
        if (!GameManager.Instance.startCheck)
        {
            //if문으로 라운드 시작 이후엔 못하게 해야됨
            if (GameManager.Instance.Round > 1)
            {
                GameManager.Instance.Round -= 1;
                RoundUpdate();
            }
            else
            {
                Debug.Log("할 수 없습니다.");
            }
        }
    }



    public void AtkBtn()
    {
        if(playerstate.gold >= currentATKCost)
        {
            playerstate.gold -= currentATKCost;
            currentATK += 5;
            currentATKCost += 100;
            playerstate.dmg += 5;
            updateUI();
        }
    }

    public void SpeedBtn()
    {
        if (playerstate.gold >= currentSpeedCost)
        {
            playerstate.gold -= currentSpeedCost;
            currentSpeed -= 0.001f;
            currentSpeedCost += 100;
            playerstate.atkSpeed -= 0.001f;
            updateUI();
        }
    }
    public void CriDBtn()
    {
        if (playerstate.gold >= currentCriDCost)
        {
            playerstate.gold -= currentCriDCost;
            currentCriD += 0.01f;
            currentCriDCost += 1000;
            playerstate.Criticaldmg += 0.01f;
            updateUI();
        }
    }
    public void CriCBtn()
    {
        if (playerstate.gold >= currentCriCCost)
        {
            playerstate.gold -= currentCriDCost;
            currentCriC += 0.01f;
            currentCriCCost += 1000;
            playerstate.criticalchance += 0.01f;
            updateUI();
        }
    }
    public void MagicBtn()
    {
        if (playerstate.gold >= currentMagicCost)
        {
            playerstate.gold -= currentMagicCost;
            currentMagic += 5;
            currentMagicCost += 100;
            playerstate.magicdmg += 5;
            updateUI();
        }
    }



}
