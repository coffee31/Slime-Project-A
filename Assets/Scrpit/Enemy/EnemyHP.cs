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

    public GameObject hpBarPrefab; // HP 바 프리팹

    private GameObject hpBarInstance; // 각 Enemy에 대한 HP 바 인스턴스

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
            Debug.Log("GameManager를 찾을 수 없습니다.");
        }

        if (hpBarPrefab != null && canvas != null)
        {
            hpBarInstance = Instantiate(hpBarPrefab, canvas.transform);
            hpBarInstance.SetActive(true); // HP 바 활성화
        }
        else
        {
            if (hpBarPrefab == null)
            {
                Debug.Log("HPbar가 없습니다.");
            }
            if (canvas == null)
            {
                Debug.Log("캔버스가 없습니다..");
            }
        }
    }

    private void LateUpdate()
    {
        if (hpBarInstance != null)
        {
            // 몬스터 위치를 기준으로 HP 바 위치 조정 (이동 및 회전)
            hpBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
            hpBarInstance.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.Log("hpBarInstance가 null입니다.");
        }
    }

    // HP 감소 및 HP 바 업데이트
    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxhp);

        // 각 Enemy의 HP 바에 대한 업데이트 호출
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

    // HP 바 업데이트
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
                Debug.Log("HPBar 없습니다.");
            }
        }
        else
        {
            Debug.Log("hpBarInstance가 null입니다.");
        }
    }
}