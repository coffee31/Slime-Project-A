using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID;
    public string Name;
    public int Cost;
    public Sprite Image;
    public bool Stackable;
    public int Count;

    public Item(int id, string name, int cost, Sprite image, bool stackable)
    {
        ID = id;
        Name = name;
        Cost = cost;
        Image = image;
        Stackable = stackable;
        Count = 1;
    }
}
