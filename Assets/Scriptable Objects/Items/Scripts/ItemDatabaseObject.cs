using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemData[] Items;
    public Dictionary<int, ItemData> GetItem = new Dictionary<int, ItemData>();

    public void OnAfterDeserialize()
    {
        GetItem = new Dictionary<int, ItemData>();

        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].ID = i;
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemData>();
    }
}
