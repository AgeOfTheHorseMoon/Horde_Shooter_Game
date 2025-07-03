using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loadout Data", menuName = "Scriptable Objects/Guns/Gun Loadout Data")]
public class GunLoadoutDataSO : ScriptableObject
{
    public GunDataSO[] Loadout;
}
