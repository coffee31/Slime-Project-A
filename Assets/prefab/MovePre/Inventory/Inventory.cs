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
                // ��ø ������ �������̸鼭 �̹� ���Կ� �ִ� ���
                foreach (Item item in Inventoryitems)
                {
                    if (item.ID == addItem.ID)
                    {
                        item.Count++; // ��ø ������ ���� ����
                        DisplayInventory();
                        Debug.Log("Item stacked");
                        return;
                    }
                }

                // ��ø ������ ������������ ���Կ� ���� ���
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
                // ��ø���� ���� �������� ���
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
        if (Index >= 0 && Index < Inventoryitems.Count) // ���� �ε����� ��ȿ���� Ȯ���մϴ�.
        {

            Slot slot = slotObjects[Index].GetComponent<Slot>();
            slot.ImageOFF(); // �̹����� ��Ȱ��ȭ�մϴ�.

            Item removedItem = Inventoryitems[Index];
            if (removedItem.Stackable)
            {
                // ��ø ������ �������̸鼭 ������ 1 �̻��� ���
                if (removedItem.Count > 1)
                {
                    removedItem.Count--; // ������ ���� ����
                }
                else
                {
                    Inventoryitems.RemoveAt(Index); // ������ ����
                    slotCount--;
                }
            }
            else
            {
                // ��ø���� ���� ������ ����
                Inventoryitems.RemoveAt(Index);
                slotCount--;
            }

            for (int i = Index; i < slotObjects.Count; i++)
            {
                Slot nextSlot = slotObjects[i].GetComponent<Slot>();
                if (i < Inventoryitems.Count)
                {
                    nextSlot.SetImage(Inventoryitems[i].Image); // �ش� ���Կ� ���ο� �̹��� ����
                }
                else
                {
                    nextSlot.ImageOFF(); // ���Կ� �������� ������ �̹����� ��Ȱ��ȭ�մϴ�.
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


