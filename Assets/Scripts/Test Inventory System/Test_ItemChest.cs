using UnityEngine;

public class Test_ItemChest : MonoBehaviour
{
    [SerializeField] private Test_Item item;
    [SerializeField] int amount = 1;
    
    private Test_Inventory inventory;

    [SerializeField] private KeyCode itemPickupKeycode = KeyCode.E;

    private bool _isInRange;
    private bool _isEmpty;

    private void Update()
    {
        if (_isInRange && Input.GetKeyDown(itemPickupKeycode) && !_isEmpty)
        {
            Test_Item itemCopy = item.GetCopy();
            if (inventory.AddItem(itemCopy))
            {
                amount--;
                if (amount == 0)
                {
                    _isEmpty = true;
                    itemCopy.Destroy();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Test_PlayerInventory inventoryScript))
        {
            _isInRange = true;
            inventory = inventoryScript.PlayerInventory;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInRange = false;
        inventory = null;
    }
}