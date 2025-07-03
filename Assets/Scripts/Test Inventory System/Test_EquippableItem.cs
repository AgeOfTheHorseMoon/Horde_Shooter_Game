using Coruk.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Buff,
    Debuff,
    SecondBuff
}

[CreateAssetMenu]
public class Test_EquippableItem : Test_Item
{

    public float StrengthBonus;
    public float SpeedBonus;
    public float HealthBonus;
    public float DamageBonus;
    [Space]
    public float StrengthPercentBonus;
    public float SpeedPercentBonus;
    public float HealthPercentBonus;
    public float DamagePercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public override Test_Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

    public void Equip(Test_PlayerInventory character)
    {
        //--------------------- Flat
        if (StrengthBonus != 0)
        {
            character.Strength.AddModifier(new StatModifier(StrengthBonus, StatModifierType.Flat, this));
        }
        if (SpeedBonus != 0)
        {
            character.Speed.AddModifier(new StatModifier(SpeedBonus, StatModifierType.Flat, this));
        }
        if (HealthBonus != 0)
        {
            character.Health.AddModifier(new StatModifier(HealthBonus, StatModifierType.Flat, this));
        }
        if (DamageBonus != 0)
        {
            character.Damage.AddModifier(new StatModifier(DamageBonus, StatModifierType.Flat, this));
        }

        //--------------------- Percentage
        if (StrengthPercentBonus != 0)
        {
            character.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModifierType.PercentageMultiplicative, this));
        }
        if (SpeedPercentBonus != 0)
        {
            character.Speed.AddModifier(new StatModifier(SpeedPercentBonus, StatModifierType.PercentageMultiplicative, this));
        }
        if (HealthPercentBonus != 0)
        {
            character.Health.AddModifier(new StatModifier(HealthPercentBonus, StatModifierType.PercentageMultiplicative, this));
        }
        if (DamagePercentBonus != 0)
        {
            character.Damage.AddModifier(new StatModifier(DamagePercentBonus, StatModifierType.PercentageMultiplicative, this));
        }

    }

    public void Unequip(Test_PlayerInventory character)
    {
        character.Strength.RemoveAllModifiersFromSource(this);
        character.Speed.RemoveAllModifiersFromSource(this);
        character.Health.RemoveAllModifiersFromSource(this);
        character.Damage.RemoveAllModifiersFromSource(this);
    }
}
