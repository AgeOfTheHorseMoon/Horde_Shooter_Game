using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Test_CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Products;

    public bool CanCraft(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if (itemContainer.ItemCount(itemAmount.Item.ID) < itemAmount.Amount)
            {
                return false;
            }
        }
        return false;
    }

    public void Craft(IItemContainer itemContainer)
    {
        if (CanCraft(itemContainer))
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                Test_Item oldItem = itemContainer.RemoveItem(itemAmount.Item.ID);
                oldItem.Destroy();
            }

            foreach (ItemAmount itemAmount in Products)
            {
                itemContainer.AddItem(itemAmount.Item.GetCopy());
            }
        }
    }
}
