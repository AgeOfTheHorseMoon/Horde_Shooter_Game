using System;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : ItemContainer
{
    [SerializeField] private List<Test_Item> startingItems;
    [SerializeField] private Transform itemsParent;

    public EventHandler<Test_BaseItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_BaseItemSlot> OnPointerExitEvent;
    public EventHandler<Test_BaseItemSlot> OnRightClickEvent;
    public EventHandler<Test_BaseItemSlot> OnBeginDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDragEvent;
    public EventHandler<Test_BaseItemSlot> OnEndDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<Test_ItemSlot>();
        }

        SetStartingItems();
    }

    private void SetStartingItems()
    {
        // Uses same variable for both loops
        int i = 0;

        // When there is an item this loop works
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i].GetCopy();
            itemSlots[i].Amount = 1;
        }

        // When there isn't an item this loop works
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
            itemSlots[i].Amount = 0;
        }
    }

}