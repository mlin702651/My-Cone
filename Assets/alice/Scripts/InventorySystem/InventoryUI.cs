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
        InventoryManager.instance.onSelectionChangeCallBack += UpdateInventorySelection; 
        InventoryManager.instance.onSelectionResetCallBack += ResetInventoryUI; 
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
        Debug.Log("UIAdditem");
    }

    public void UpdateInventoryRemove(ItemBase item){
        slots[(int)GetSameSlot(item)].RemoveItem();
    }
    public void UpdateInventorySelection(int newSelection,int currentSelection){
        slots[currentSelection].UpdateFrame(false);
        slots[newSelection].UpdateFrame(true);
        // for (int i = 0; i < slots.Length; i++)
        // {
        //     if(i == currentSelection) slots[i].UpdateFrame(true);
        //     else slots[i].UpdateFrame(false);
        // }
        
    }
    void OnDisable()
    {
        Debug.Log("PrintOnDisable: script was disabled");
    }

    public void ResetInventoryUI(int currentSelection){
        slots[currentSelection].UpdateFrame(false);
    }
}
