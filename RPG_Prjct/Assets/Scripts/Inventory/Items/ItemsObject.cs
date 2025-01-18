using System;
using UnityEngine;
using Random = UnityEngine.Random;


public enum ItemType
{
    Food,
    Equipment,
    Weapon,
    Trash
}

public enum Attributes
{
    Health,
    Mana,
    Stamina,
    Speed,
    Strength
}

public class ItemsObject : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Icon;
    [TextArea(20,20)] public string Description;
    public ItemType type;
    public ItemBuff[] Buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]

public class Item
{
    public int Id;
    public string Name;
    public ItemBuff[] Buff;

    public Item(ItemsObject item)
    {
        Id = item.ID;
        Name = item.Name;
        Buff = new ItemBuff[item.Buffs.Length];
        for (int i = 0; i < Buff.Length; i++)
        {
            Buff[i] = new ItemBuff(item.Buffs[i].MinValue, item.Buffs[i].MaxValue)
            {
                Attribute = item.Buffs[i].Attribute

            };
        }
    }
}

[System.Serializable]

public class ItemBuff
{
    public Attributes Attribute;
    public int Value;
    public int MaxValue;
    public int MinValue;

    public ItemBuff(int min, int max)
    {
        MinValue = min;
        MaxValue = max;
        GenerateValue();
    }

    private void GenerateValue()
    {
        Value = Random.Range(MinValue, MaxValue);
    }
}