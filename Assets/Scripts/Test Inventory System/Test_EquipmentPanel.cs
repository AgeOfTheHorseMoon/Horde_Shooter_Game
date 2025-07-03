using System;
using UnityEngine;

public class Test_EquipmentPanel : MonoBehaviour
{
    [SerializeField] private Transform _equipmentSlotsParent;
    [SerializeField] private Test_EquipmentSlot[] _equipmentSlots;

    public EventHandler<Test_BaseItemSlot> OnPointerEnterEvent;
    public EventHandler<Test_BaseItemSlot> OnPointerExitEvent;
    public EventHandler<Test_BaseItemSlot> OnRightClickEvent;
    public EventHandler<Test_BaseItemSlot> OnBeginDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDragEvent;
    public EventHandler<Test_BaseItemSlot> OnEndDragEvent;
    public EventHandler<Test_BaseItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            _equipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            _equipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            _equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
            _equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            _equipmentSlots[i].OnDragEvent += OnDragEvent;
            _equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            _equipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        _equipmentSlots = _equipmentSlotsParent.GetComponentsInChildren<Test_EquipmentSlot>();
    }

    public bool AddItem(Test_EquippableItem item, out Test_EquippableItem previousItem)
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            if (_equipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                previousItem = (Test_EquippableItem)_equipmentSlots[i].Item;
                _equipmentSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(Test_EquippableItem item)
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            if (_equipmentSlots[i].Item == item)
            {
                _equipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}