using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_EquipmentSlot : Test_ItemSlot
{
    public EquipmentType EquipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Test_Item item)
    {
        if (item == null)
        {
            return true;
        }

        var equippableItem = item as Test_EquippableItem;
        return equippableItem != null && equippableItem.EquipmentType == EquipmentType;
    }
}
