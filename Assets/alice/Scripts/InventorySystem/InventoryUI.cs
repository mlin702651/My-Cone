using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //[HideInInspector]
    public InventorySlot[] slots;

    private void Start() {
        slots = GetComponentsInChildren<InventorySlot>();
        InventoryManager.instance.onItemAddCallBack += UpdateInventoryAdd; 
        InventoryManager.instance.onItemRemoveCallBack += UpdateInventoryRemove; 
        //只要onItemAddCallBack被呼叫 就會執行UpdateInventoryAdd
    }
    private int? GetNextEmptySlot(){
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].slotItem == null) return i;
        }
        return null;
    }
    private int? GetSameSlot(ItemBase item){
        for (int i = slots.Length-1; i >= 0 ; i--)
        {
            if(slots[i].slotItem != null) {
                if(slots[i].slotItem == item) return i;
            }
        }
        return null;
    }
    public void UpdateInventoryAdd(ItemBase item){
        slots[(int)GetNextEmptySlot()].AddItem(item);
    }

    public void UpdateInventoryRemove(ItemBase item){
        slots[(int)GetSameSlot(item)].RemoveItem();
    }
}
