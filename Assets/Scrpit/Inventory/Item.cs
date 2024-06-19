using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID;
    public string Name;
    public Sprite Image;
    public bool Stackable;
    public int Count;


    public Item(int id, string name, Sprite image, bool stackable)
    {
        ID = id;
        Name = name;
        Image = image;
        Stackable = stackable;
        Count = 1;
    }
}

public class Weapon : Item
{
    public int Damage;

    public Weapon(int id, string name, Sprite image, bool stackable, int damage)
        : base(id, name, image, stackable)
    {
        Damage = damage;
    }
}

public class Glove : Item
{
    public float atkSpeed;

    public Glove(int id, string name, Sprite image, bool stackable, float speed) : base(id, name, image, stackable)
    {
        atkSpeed = speed;
    }

}

