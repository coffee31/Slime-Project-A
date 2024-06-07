using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item(0,"Sword", 100, Resources.Load<Sprite>("sword"),false));
        items.Add(new Item(1,"Shield", 100, Resources.Load<Sprite>("Shield"),false));
        items.Add(new Item(2, "Potion", 100, Resources.Load<Sprite>("Potion"), true));
    }

    public Item GetItemID(int id)
    {
        return items.Find(Item => Item.ID == id);
    }

}
