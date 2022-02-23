using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public ItemBase slotItem;
    public Image selectedFrame;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color originalColor;
    public void AddItem(ItemBase newitem){
        icon.overrideSprite = newitem.itemIcon;
        slotItem = newitem;
    }

    public void RemoveItem(){
        icon.overrideSprite = null;
        slotItem = null;
    }

    public void UpdateFrame(bool isActive){
        if(isActive)selectedFrame.color = hoverColor;
        else selectedFrame.color = originalColor;
    }
}
