using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectionUI : MonoBehaviour
{
    public InventorySlot[] slots;
    public GameObject parentUI;
    
    

    private void Start() {
        slots = GetComponentsInChildren<InventorySlot>();
        parentUI.SetActive(false);
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
        var remainder = GetRemainder(item);

        if(remainder == 0){
            remainder = item.maxStackSize;
        }

        if(remainder ==1){
            print("add slot");
            slots[(int)GetNextEmptySlot()].AddItem(item);
            if(item is HatProp){ //如果是帽子幫她註冊一個可以戴帽子的事件
                HatProp hatProp = item as HatProp;
                slots[(int)GetSameSlot(item)].GetComponent<UnityItemEventHandler>().unityEvent = hatProp.itemEvent;
            }

        }
        else{
            slots[(int)GetSameSlot(item)].amount.text = remainder.ToString();
        }

        //Debug.Log("UIAdditem");
    }

    public void UpdateInventoryRemove(ItemBase item){
        var remainder = GetRemainder(item);

        if(remainder == 0){
            remainder = item.maxStackSize;
        }
        if(remainder == item.maxStackSize){
            slots[(int)GetSameSlot(item)].amount.text = "";
            slots[(int)GetSameSlot(item)].RemoveItem();
        }
        else{
            slots[(int)GetSameSlot(item)].amount.text = remainder.ToString();
        }
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
    
    public void ResetInventoryUI(int currentSelection){
        slots[currentSelection].UpdateFrame(false);
    }

    private int GetRemainder(ItemBase newItem){
        var itemCount = InventoryManager.instance.collection.Count(x => x== newItem); //傳回背包裡 這種newItem的數量
        return itemCount % newItem.maxStackSize;
    }

    private ItemBase GetSelectedItem(int newSelection){
        return slots[newSelection].slotItem;
    }

    private void OpenEquipMenu(int currentSelection){
        slots[currentSelection].OpenEquipWindow();
    }

    private void CheckEquipItem(int currentSelection){
        slots[currentSelection].EquipItem();
    }
}
