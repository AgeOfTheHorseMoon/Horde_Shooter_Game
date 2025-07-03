using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Scriptable Objects/Guns/Gun Data")]
public class GunDataSO : ScriptableObject
{
    public GameObject ProjectilePrefab;
    public GameObject GunPrefab;

    public int Damage;
    public int ProjectilesPerMinute;
    
    public float Range;
    public float ProjectileSpeed = 10f;
    public float ProjectileLifeTime = 5f;
}
