using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemBase item;
    public QuestProp questProp;

    public override void Interact()
    {
        InventoryManager.instance.AddItem(item);
        UIGuide.instance.uiNPCTalk.SetActive(false);
        if(questProp!=null){
            GameManager.instance.onPlayerCollectCallBack?.Invoke(questProp);
        }
        if(item is CollectProp) return;
        gameObject.SetActive(false);
    }
}
