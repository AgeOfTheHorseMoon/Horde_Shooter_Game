using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public int ID, Amount;
    public Item Item;

    public InventorySlot(int id, Item item, int amount)
    {
        ID = id;
        Item = item;
        Amount = amount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }
}

[CreateAssetMenu(fileName ="InventoryName", menuName ="Inventory System/Inventory")]
public class InventoryScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    public event EventHandler OnItemListChanged;

    public string SavePath;

    ItemDatabaseObject ItemDatabase;
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        ItemDatabase = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Item Database.asset",typeof(ItemDatabaseObject));
#else
        ItemDatabase = Resources.Load<ItemDatabaseObject>("Item Database");
#endif
    }

    public void AddItem(Item item, int amount)
    {
        if (item.Buffs.Length > 0 && !item.ItemData.IsStackable)
        {
            Container.Add(new InventorySlot(item.ID, item, amount));
            return;
        }

        if (item.ItemData.IsStackable)
        {
            foreach (InventorySlot slot in Container)
            {
                if (slot.Item.ID == item.ID) // if not working, use item.itemdata (temporarily)
                {
                    slot.AddAmount(amount);
                    RefreshInventoryDisplay();
                    return;
                }
            }
            Container.Add(new InventorySlot(item.ID, item, amount));
        }
        else
        {
            Container.Add(new InventorySlot(item.ID, item, amount));
        }
        RefreshInventoryDisplay();
    }

    private void RefreshInventoryDisplay()
    {
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<InventorySlot> GetItemList()
    {
        return Container;
    }

    /// -------------------------------------
    /// Saving and Loading
    
    public void Save()
    {
        string savedata = JsonUtility.ToJson(this, true);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, SavePath));
        binaryFormatter.Serialize(file, savedata);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, SavePath)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, SavePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(file).ToString(), this);
            file.Close();
        }
        RefreshInventoryDisplay();
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        foreach (InventorySlot slot in Container)
        {
            slot.Item = new Item(ItemDatabase.GetItem[slot.ID]); // Get item from ID
        }
    }
}