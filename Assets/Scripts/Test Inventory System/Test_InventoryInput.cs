using UnityEngine;

public class Test_InventoryInput : MonoBehaviour
{
    [SerializeField] private GameObject characterPanelGameObject; // includes both equipment and inventory
    [SerializeField] private GameObject equipmentPanelGameObject;
    [SerializeField] private GameObject inventoryPanelGameObject;
    [SerializeField] private GameObject craftingPanelGameObject;
    [SerializeField] private KeyCode[] toggleEquipmentPanelKeys;
    [SerializeField] private KeyCode[] toggleInventoryKeys;
    [SerializeField] private KeyCode[] toggleCraftingPanelKeys;

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < toggleEquipmentPanelKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleEquipmentPanelKeys[i]))
            {
                if (!characterPanelGameObject.activeInHierarchy)
                {   // Nothing is open
                    characterPanelGameObject.SetActive(true);
                    equipmentPanelGameObject.SetActive(true);

                    ShowMouseCursor();
                }
                else if (!equipmentPanelGameObject.activeInHierarchy && inventoryPanelGameObject.activeInHierarchy)
                {   // Inventory is open
                    equipmentPanelGameObject.SetActive(true);
                }
                else
                {
                    characterPanelGameObject.SetActive(false);
                    equipmentPanelGameObject.SetActive(false);

                    HideMouseCursor();
                }
                break;
            }
        }

        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                if (!characterPanelGameObject.activeInHierarchy)
                {   // Nothing is open
                    characterPanelGameObject.SetActive(true);
                    inventoryPanelGameObject.SetActive(true);

                    ShowMouseCursor();
                }
                else if (!equipmentPanelGameObject.activeInHierarchy && inventoryPanelGameObject.activeInHierarchy)
                {   // Only Inventory is open
                    characterPanelGameObject.SetActive(false);
                    inventoryPanelGameObject.SetActive(false);
                    craftingPanelGameObject.SetActive(false);
                    HideMouseCursor();
                }
                else if (equipmentPanelGameObject.activeInHierarchy && !inventoryPanelGameObject.activeInHierarchy)
                {   // Only Equipment is open
                    inventoryPanelGameObject.SetActive(true);
                }
                else
                {   // Everything is open
                    characterPanelGameObject.SetActive(false);
                    inventoryPanelGameObject.SetActive(false);
                    equipmentPanelGameObject.SetActive(false);
                    craftingPanelGameObject.SetActive(false);

                    HideMouseCursor();
                }
                break;
            }
        } 
        
        for (int i = 0; i < toggleCraftingPanelKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleCraftingPanelKeys[i]))
            {
                if (inventoryPanelGameObject.activeInHierarchy && !craftingPanelGameObject.activeInHierarchy)
                {
                    craftingPanelGameObject.SetActive(true);
                }
                else
                {
                    craftingPanelGameObject.SetActive(false);
                }
                break;
            }
        }
    }

    public void ToggleEquipmentPanel() // For the "Equipment" button
    {
        equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}