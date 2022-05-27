using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemBase item;
    public CollectableProfile collectableProfile;

    public override void Interact()
    {
        InventoryManager.instance.AddItem(item);
        UIGuide.instance.uiNPCTalk.SetActive(false);
        if(collectableProfile!=null){
            GameManager.instance.onPlayerCollectCallBack?.Invoke(collectableProfile);
        }
        if(item is CollectProp) return;
        gameObject.SetActive(false);
    }
}
