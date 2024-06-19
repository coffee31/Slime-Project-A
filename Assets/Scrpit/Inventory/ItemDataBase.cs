using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    int akt;

    void Start()
    {
        items.Add(new Item(0, "Potion", Resources.Load<Sprite>("Potion"),true));
        items.Add(new Weapon(1,"Sword",  Resources.Load<Sprite>("sword"),false,5));
        items.Add(new Glove(2, "Knuckles", Resources.Load<Sprite>("knuckles"), false, 0.01f));
    }

    public Item GetItemID(int id)
    {
        return items.Find(Item => Item.ID == id);
    }


}
