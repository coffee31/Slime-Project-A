using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAction : MonoBehaviour
{
    public Inventory inven;

    void Start()
    {
        inven = FindObjectOfType<Inventory>();
    }

    public void ItemAdd()
    {
        if (GameManager.Instance.Jewel >= 100)
        {
            GameManager.Instance.Jewel -= 100;
            inven.Additem(Random.Range(0, 3));
        }
        else
            Debug.Log("보석이 부족합니다.");
    }

    public void RemoveLastItem()
    {
        if (GameManager.Instance.Jewel >= 10)
        {
            GameManager.Instance.Jewel -= 10;
            int lastIndex = inven.Inventoryitems.Count - 1;
            inven.RemoveItem(lastIndex);

        }
        else
            Debug.Log("보석이 부족합니다.");

    }



}
