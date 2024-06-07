using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public List<Item> Inventoryitems = new List<Item>();
    public ItemDataBase ItemDataBase;

    public GameObject slotPrefab;
    public Transform slotsParent;

    private List<GameObject> slotObjects = new List<GameObject>();


    int slotCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        ItemDataBase = FindObjectOfType<ItemDataBase>();
        slotCount = 0;
        Initslot(30);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.Instance.Jewel += 200;
            Debug.Log(GameManager.Instance.Jewel);

        }
    }

    void Initslot(int slotcnt)
    {
        for(int i = 0; i < slotcnt; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
            slotObjects.Add(slot);
            Slot slotScript = slot.GetComponent<Slot>();
            if (slotScript != null)
            {
                slotScript.indexnum = i;
            }

        }
    }


    public void Additem(int id)
    {
        Item addItem = ItemDataBase.GetItemID(id);
        if (addItem != null)
        {
            if (addItem.Stackable)
            {
                // 중첩 가능한 아이템이면서 이미 슬롯에 있는 경우
                foreach (Item item in Inventoryitems)
                {
                    if (item.ID == addItem.ID)
                    {
                        item.Count++; // 중첩 아이템 개수 증가
                        DisplayInventory();
                        Debug.Log("Item stacked");
                        return;
                    }
                }

                // 중첩 가능한 아이템이지만 슬롯에 없는 경우
                if (slotCount < 30)
                {
                    Inventoryitems.Add(addItem);
                    slotCount++;
                    DisplayInventory();
                    Debug.Log("Success");
                    return;
                }
            }
            else
            {
                // 중첩되지 않은 아이템인 경우
                if (slotCount < 30)
                {
                    Inventoryitems.Add(addItem);
                    slotCount++;
                    DisplayInventory();
                    Debug.Log("Success");
                    return;
                }
            }
        }

        Debug.Log("Fail");
    }

    public void RemoveItem(int Index)
    {
        if (Index >= 0 && Index < Inventoryitems.Count) // 슬롯 인덱스가 유효한지 확인합니다.
        {

            Slot slot = slotObjects[Index].GetComponent<Slot>();
            slot.ImageOFF(); // 이미지를 비활성화합니다.

            Item removedItem = Inventoryitems[Index];
            if (removedItem.Stackable)
            {
                // 중첩 가능한 아이템이면서 개수가 1 이상인 경우
                if (removedItem.Count > 1)
                {
                    removedItem.Count--; // 아이템 개수 감소
                }
                else
                {
                    Inventoryitems.RemoveAt(Index); // 아이템 삭제
                    slotCount--;
                }
            }
            else
            {
                // 중첩되지 않은 아이템 삭제
                Inventoryitems.RemoveAt(Index);
                slotCount--;
            }

            for (int i = Index; i < slotObjects.Count; i++)
            {
                Slot nextSlot = slotObjects[i].GetComponent<Slot>();
                if (i < Inventoryitems.Count)
                {
                    nextSlot.SetImage(Inventoryitems[i].Image); // 해당 슬롯에 새로운 이미지 설정
                }
                else
                {
                    nextSlot.ImageOFF(); // 슬롯에 아이템이 없으면 이미지를 비활성화합니다.
                }
            }

            DisplayInventory();
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Invalid slot index");
        }
    }


    public void ItemList()
    {
        foreach (Item item in Inventoryitems)
        {
            if (item.Stackable)
            {
                Debug.Log("Item : " + item.Name + ", Count: " + item.Count);
            }
            else
            {
                Debug.Log("Item : " + item.Name);
            }
        }

    }


    public void DisplayInventory()
    {
        for (int i = 0; i < Inventoryitems.Count; i++)
        {
            Slot slotScript = slotObjects[i].GetComponent<Slot>();
            if (slotScript != null && Inventoryitems[i].Image != null)
            {
                slotScript.SetImage(Inventoryitems[i].Image);
            }
        }
    }
}


