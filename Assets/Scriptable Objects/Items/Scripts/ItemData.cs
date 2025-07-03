using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData : ScriptableObject
{
    public Sprite Sprite;

    public bool IsStackable;
    public int Amount = 1, ID;

    [TextArea(15,20)]
    public string Description = "Description of this item is missing!"; // Use if needed

    public ItemBuff[] Buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

