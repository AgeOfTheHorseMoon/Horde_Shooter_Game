using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Test_BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amountText;

    public EventHandler<Test_BaseItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_BaseItemSlot> OnPointerExitEvent;
    public EventHandler<Test_BaseItemSlot> OnRightClickEvent;

    private Test_Item _item;
    public Test_Item Item
    {
        get { return _item; }

        set
        {
            _item = value;
            if (_item == null)
            {
                image.color = Color.clear;
            }
            else
            {
                image.sprite = _item.Icon;
                image.color = Color.white;
            }
        }
    }

    private int _amount;
    public int Amount
    {
        get
        {
            return _amount;
        }

        set
        {
            _amount = value;

            if (amountText != null)
            {
                amountText.enabled = _item != null && _item.MaximumStackSize > 1 && _amount > 1;
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }
        }
    }


    protected virtual void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        
        if (amountText == null)
        {
            amountText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public virtual bool CanReceiveItem(Test_Item item)
    {
        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this, this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(this, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke(this, this);
    }
}