using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] InventoryScriptableObject _inventory;
    [SerializeField] Transform _itemSlotContainer;
    [SerializeField] Transform _itemSlotTemplate;
    [SerializeField] float itemSlotCellSize = 80f;

    private void Start()
    {
        _inventory.OnItemListChanged += Inventory_OnItemListChanged;
        // Don't show on game start (find better way)
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        RefreshInventoryItems();
    }

    private void OnApplicationQuit()
    {
        _inventory.OnItemListChanged -= Inventory_OnItemListChanged;

    }
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    void RefreshInventoryItems()
    {
        foreach (Transform child in _itemSlotContainer) // if child has a template don't destroy, will make a new one
        {
            if (child == _itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0, y = 0;
        foreach(InventorySlot inventorySlot in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = inventorySlot.Item.ItemData.Sprite;

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("TextAmount").GetComponent<TextMeshProUGUI>();
            if (inventorySlot.Amount > 1)
            {
                uiText.SetText(inventorySlot.Amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
