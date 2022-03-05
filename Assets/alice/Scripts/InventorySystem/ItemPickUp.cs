using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemBase item;

    public override void Interact()
    {
        InventoryManager.instance.AddItem(item);
        Destroy(gameObject);
    }
}
