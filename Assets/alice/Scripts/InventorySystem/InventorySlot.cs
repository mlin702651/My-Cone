using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    [HideInInspector]public ItemBase slotItem;
    public Image selectedFrame;
    public Text amount;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color originalColor;
    [SerializeField] private GameObject equipWindow;

    public bool ifSelectEquip = true;
    public void AddItem(ItemBase newitem){
        icon.overrideSprite = newitem.itemIcon;
        slotItem = newitem;
    }

    public void RemoveItem(){
        icon.overrideSprite = null;
        amount.text = "";
        slotItem = null;
    }

    public void UpdateFrame(bool isActive){
        if(isActive)selectedFrame.color = hoverColor;
        else selectedFrame.color = originalColor;
    }
    public void OpenEquipWindow(){
        if(slotItem is HatProp){
            equipWindow.SetActive(true);
            InventoryManager.instance.inEquipItem = true;

        }
    }

    public void SelectConfirmButton(){

    }
    public void EquipItem(){
        if(ifSelectEquip){
            HatProp hatProp = slotItem as HatProp;
            //hatProp.itemEvent.Invoke();
            Debug.Log(slotItem.itemName + "Equiped!");
        } 
        
        equipWindow.SetActive(false);
        InventoryManager.instance.inEquipItem = false;

    }
}
