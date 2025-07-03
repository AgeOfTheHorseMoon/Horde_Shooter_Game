using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test_ItemSlot : Test_BaseItemSlot, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler 
{
    public EventHandler<Test_BaseItemSlot> OnBeginDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDragEvent;
    public EventHandler<Test_BaseItemSlot> OnEndDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDropEvent;

    public override bool CanReceiveItem(Test_Item item)
    {
        return true; 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(this, this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this, this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this, this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this, this);
    }
}
