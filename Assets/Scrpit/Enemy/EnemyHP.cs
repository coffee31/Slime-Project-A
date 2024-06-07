using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    GameManager manager;

    private int maxhp;
    public int MAXHP => maxhp;

    private int hp;

    public int HP
    {
        get { return hp; }
        set { hp = Mathf.Clamp(value, 0, maxhp); }
    }

    public GameObject hpBarPrefab; // HP �� ������

    private GameObject hpBarInstance; // �� Enemy�� ���� HP �� �ν��Ͻ�

    public Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        manager = GameManager.Instance;
        if (manager != null)
        {
            maxhp = 40 * manager.Round;
            hp = maxhp;
        }
        else
        {
            Debug.Log("GameManager�� ã�� �� �����ϴ�.");
        }

        if (hpBarPrefab != null && canvas != null)
        {
            hpBarInstance = Instantiate(hpBarPrefab, canvas.transform);
            hpBarInstance.SetActive(true); // HP �� Ȱ��ȭ
        }
        else
        {
            if (hpBarPrefab == null)
            {
                Debug.Log("HPbar�� �����ϴ�.");
            }
            if (canvas == null)
            {
                Debug.Log("ĵ������ �����ϴ�..");
            }
        }
    }

    private void LateUpdate()
    {
        if (hpBarInstance != null)
        {
            // ���� ��ġ�� �������� HP �� ��ġ ���� (�̵� �� ȸ��)
            hpBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
            hpBarInstance.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.Log("hpBarInstance�� null�Դϴ�.");
        }
    }

    // HP ���� �� HP �� ������Ʈ
    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxhp);

        // �� Enemy�� HP �ٿ� ���� ������Ʈ ȣ��
        UpdateHPBar();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (hpBarInstance != null)
        {
            Destroy(hpBarInstance);
        }
    }

    // HP �� ������Ʈ
    private void UpdateHPBar()
    {
        if (hpBarInstance != null)
        {
            HealthBar hpBarScript = hpBarInstance.GetComponent<HealthBar>();
            if (hpBarScript != null)
            {
                hpBarScript.HPbarValue(hp, maxhp);
            }
            else
            {
                Debug.Log("HPBar �����ϴ�.");
            }
        }
        else
        {
            Debug.Log("hpBarInstance�� null�Դϴ�.");
        }
    }
}