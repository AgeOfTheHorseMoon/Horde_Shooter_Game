using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon Item", menuName = "Inventory System/Items/Weapons")]
public class WeaponScriptableObject : ItemData
{
    // Don't need to use one of these
    public int Damage = 10;
    public float Range = 0; 
    public int BulletsPerMinute = 200;

}
