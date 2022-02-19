using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<ItemBase> inventory = new List<ItemBase>();

    //Delegate: only trigger when sth happen.
    public delegate void OnItemAddCallBack(ItemBase item);
    public OnItemAddCallBack onItemAddCallBack;
    public delegate void OnItemRemoveCallBack(ItemBase item);
    public OnItemRemoveCallBack onItemRemoveCallBack;
    public delegate void OnSelectionChangeCallBack(int newSelection, int currentSelection);
    public OnSelectionChangeCallBack onSelectionChangeCallBack;
    public delegate void OnSelectionResetCallBack(int currentSelection);
    public OnSelectionResetCallBack onSelectionResetCallBack;

    public bool inBackpack;

    //selection
    private int currentSelection = 0;
    private int newSelection = 0;
    private ItemBase currentSelectedItem;

    //reference
    [SerializeField]private GameObject backpackUI;
    [SerializeField]private Image selectedItemImage;
    [SerializeField]private Text selectedItemName;
    [SerializeField]private Text selectedItemDescription;
    private void Awake(){
        if(instance != null){
            Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;
    }

    private void Start() {
        //backpackUI.SetActive(true);
        backpackUI.SetActive(false);
    }

    public void AddItem(ItemBase item){
        Debug.Log("add");
        inventory.Add(item);
        if(onItemAddCallBack != null) {
            onItemAddCallBack.Invoke(item);
            
        }
        //ResetBackpack();
    }

    public void RemoveItem(ItemBase item){
        inventory.Remove(item);
        if(onItemRemoveCallBack != null){
            onItemRemoveCallBack.Invoke(item);
        }
        //ResetBackpack();
    }

    private void Update() {
        if(MenuManager.instance.inMenu){
            backpackUI.SetActive(false); //關背包
            return; //在選單就不要動!
        }
        if(InputSystem.instance.BackpackPressDown){
            backpackUI.SetActive(!backpackUI.activeSelf); //開關背包
            if(backpackUI.activeSelf){ 
                ResetBackpack(); //打開背包要重置頁面
                inBackpack = true;
            }
            else inBackpack = false; 
            InputSystem.instance.BackpackPressDown = false;
        }
        if(backpackUI.activeSelf){
            HandleSelection();
        }
    }

    void ResetBackpack(){
        onSelectionResetCallBack.Invoke(currentSelection);
        currentSelection = 0;
        newSelection = 0;
        if(inventory.Count>=1){ //背包不是空的就預設第一個物品
            currentSelectedItem = inventory[0];
            selectedItemImage.overrideSprite = currentSelectedItem.itemIcon;
            selectedItemName.text = currentSelectedItem.itemName;
            selectedItemDescription.text = currentSelectedItem.itemDescription;
            onSelectionChangeCallBack.Invoke(newSelection,currentSelection);
        }
        else{
            selectedItemImage.overrideSprite = null;
            selectedItemName.text = "";
            selectedItemDescription.text = "";
        }
    }

    void HandleSelection(){
        if(InputSystem.instance.MenuSelectDownPressDown){ //按往下的鈕
            InputSystem.instance.MenuSelectDownPressDown = false;
            Debug.Log("down");
            newSelection = (currentSelection+3<inventory.Count)?currentSelection+3:currentSelection; //如果有下一排 選下面那個
            UpdateSelection();
        }
        else if(InputSystem.instance.MenuSelectUpPressDown){
            InputSystem.instance.MenuSelectUpPressDown = false;
            Debug.Log("up");
            newSelection = (currentSelection-3>=0)?currentSelection-3:currentSelection; //如果有上一排 選上面那個
            UpdateSelection();
        }
        else if(InputSystem.instance.MenuSelectRightPressDown){
            InputSystem.instance.MenuSelectRightPressDown = false;
            Debug.Log("Right");
            newSelection = ((currentSelection+1)<inventory.Count)?currentSelection+1:currentSelection; //如果有上一排 選上面那個
            UpdateSelection();
        }
        else if(InputSystem.instance.MenuSelectLeftPressDown){
            InputSystem.instance.MenuSelectLeftPressDown = false;
            Debug.Log("Left");
            newSelection = ((currentSelection-1)>=0)?currentSelection-1:currentSelection; //如果有上一排 選上面那個
            UpdateSelection();
        }
        
    }

    void UpdateSelection(){
        onSelectionChangeCallBack.Invoke(newSelection,currentSelection);
        currentSelectedItem = inventory[newSelection];
        selectedItemImage.overrideSprite = currentSelectedItem.itemIcon;
        selectedItemName.text = currentSelectedItem.itemName;
        selectedItemDescription.text = currentSelectedItem.itemDescription;
        currentSelection = newSelection;
    }



    
}
