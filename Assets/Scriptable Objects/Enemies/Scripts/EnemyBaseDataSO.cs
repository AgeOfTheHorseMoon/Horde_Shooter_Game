using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Enemy Data", menuName ="Scriptable Objects/Enemies/Enemy Data")] 
public class EnemyBaseDataSO : ScriptableObject
{
    public Sprite Sprite;
    public float BaseHealth;
    public float BaseDamage;
    public float BaseMovementSpeed;
    public float BaseExperience;
}
