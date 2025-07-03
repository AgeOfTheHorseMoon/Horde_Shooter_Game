using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CraftingRecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform arrowParent;
    [SerializeField] Test_BaseItemSlot[] ItemSlots;

    [Header("Public Variables")]
    public ItemContainer ItemContainer; // inventory

    private Test_CraftingRecipe _craftingRecipe;
    public Test_CraftingRecipe CraftingRecipe
    {
        get { return _craftingRecipe; }
        set { SetCraftingRecipe(value); }
    }

    public EventHandler<Test_BaseItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_BaseItemSlot> OnPointerExitEvent;

    private void OnValidate()
    {
        ItemSlots = GetComponentsInChildren<Test_BaseItemSlot>(includeInactive: true);
    }

    private void Start()
    {
        foreach (Test_BaseItemSlot itemSlot in ItemSlots)
        {
            itemSlot.OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlot.OnPointerExitEvent += OnPointerExitEvent;
        }
    }

    public void OnCraftButtonClick()
    {
        if (_craftingRecipe != null && ItemContainer != null)
        {
            if (_craftingRecipe.CanCraft(ItemContainer))
            {
                if (!ItemContainer.IsFull())
                {
                    _craftingRecipe.Craft(ItemContainer);
                }
                else
                {
                    Debug.LogError("Inventory is full!");
                }
            }
            else
            {
                Debug.LogError("You don't have the required materials!");
            }
        }
    }

    private void SetCraftingRecipe(Test_CraftingRecipe newCraftingRecipe)
    {
        _craftingRecipe = newCraftingRecipe;

        if (_craftingRecipe != null)
        {
            int slotIndex = 0;
            slotIndex = SetSlots(_craftingRecipe.Materials, slotIndex);
            arrowParent.SetSiblingIndex(slotIndex);
            slotIndex = SetSlots(_craftingRecipe.Products, slotIndex);

            for (int i = slotIndex; i < ItemSlots.Length; i++)
            {
                ItemSlots[i].transform.parent.gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex) // you can receive both array or list by IList
    {
        for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
        {
            ItemAmount itemAmount = itemAmountList[i];
            Test_BaseItemSlot itemSlot = ItemSlots[slotIndex];

            itemSlot.Item = itemAmount.Item;
            itemSlot.Amount = itemAmount.Amount;
            itemSlot.transform.parent.gameObject.SetActive(true);
        }
        return slotIndex;
    }
}
