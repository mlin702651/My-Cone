using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemBase item;

    public override void Interact()
    {
        InventoryManager.instance.AddItem(item);
        if(item is CollectProp) return;
        Destroy(gameObject);
    }
}
