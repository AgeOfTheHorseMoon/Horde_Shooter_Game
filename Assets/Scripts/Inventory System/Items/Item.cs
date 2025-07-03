using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemData ItemData;
    public string ItemName;
    public int ID;
    public ItemBuff[] Buffs;

    public Item (ItemData item)
    {
        ItemName = item.name;
        ID = item.ID;
        ItemData = item;
        
        Buffs = new ItemBuff[item.Buffs.Length];
        for (int i = 0; i < Buffs.Length; i++)
        {
            Buffs[i] = new ItemBuff(item.Buffs[i].Min, item.Buffs[i].Max);
            Buffs[i].Attribute = item.Buffs[i].Attribute;
        }
    }
}

public enum Attributes
{
    Health,
    Damage
}

[System.Serializable]
public class ItemBuff
{
    public Attributes Attribute;
    public int Value, Min, Max;

    public ItemBuff(int min, int max)
    {
        Min = min;
        Max = max;
        GenerateValue();
    }

    void GenerateValue() => Value = UnityEngine.Random.Range(Min, Max);
}