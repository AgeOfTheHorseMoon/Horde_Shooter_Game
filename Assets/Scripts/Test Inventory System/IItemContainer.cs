using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemContainer
{
    int ItemCount(string itemID);
    Test_Item RemoveItem(string itemID);
    bool RemoveItem(Test_Item item);
    bool AddItem(Test_Item item);
    bool IsFull();
}
