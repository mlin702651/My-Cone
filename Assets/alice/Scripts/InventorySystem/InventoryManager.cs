using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<ItemBase> inventory = new List<ItemBase>();

    //Delegate: only trigger when sth happen.
    public delegate void OnItemAddCallBack(ItemBase item);
    public OnItemAddCallBack onItemAddCallBack;
    public delegate void OnItemRemoveCallBack(ItemBase item);
    public OnItemRemoveCallBack onItemRemoveCallBack;
    private void Awake(){
        if(instance != null){
            Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;
    }

    public void AddItem(ItemBase item){
        inventory.Add(item);
        if(onItemAddCallBack != null) {
            onItemAddCallBack.Invoke(item);
            
        }
    }

    public void RemoveItem(ItemBase item){
        inventory.Remove(item);
        if(onItemRemoveCallBack != null){
            onItemRemoveCallBack.Invoke(item);
        }
    }

    
}
