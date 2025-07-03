using System;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Test_Item Item;

    [Range(1, 99)]
    public int Amount;
}