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

    public delegate void OnMenuPageChangeCallBack(int newMenuPage);
    public OnMenuPageChangeCallBack onMenuPageChangeCallBack;

    public bool inBackpack;
    public int currentMenuPage = 0;

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
        onMenuPageChangeCallBack += CheckOpenMenu; 
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
        if(PauseMenuManager.instance.inPauseMenu){
            backpackUI.SetActive(false); //關背包
            return; //在選單就不要動!
        }
        if(InputSystem.instance.BackpackPressDown){
            backpackUI.SetActive(!backpackUI.activeSelf); //開關背包
            currentMenuPage = 0;
            onMenuPageChangeCallBack.Invoke(currentMenuPage);
            Time.timeScale = backpackUI.activeSelf? 0f:1f;
            if(backpackUI.activeSelf){ 
                ResetBackpack(); //打開背包要重置頁面
                inBackpack = true;
            }
            else inBackpack = false; 
            InputSystem.instance.BackpackPressDown = false;
        }
        if(backpackUI.activeSelf){
            HandleSelection();
            HandleCurrentMenuPage();
        }
    }

    void ResetBackpack(){
        currentMenuPage = 0; //打開第一頁都會是背包 之後應該要包到外面去

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

    void HandleCurrentMenuPage(){
        if(InputSystem.instance.MenuSwitchPagePlusPressDown){
            InputSystem.instance.MenuSwitchPagePlusPressDown = false;
            currentMenuPage  = ((currentMenuPage+1)<=3) ? currentMenuPage+1 : 0;
            onMenuPageChangeCallBack.Invoke(currentMenuPage);
                    
            Debug.Log(currentMenuPage);
        }
        else if(InputSystem.instance.MenuSwitchPageMinusPressDown){
            InputSystem.instance.MenuSwitchPageMinusPressDown = false;
            currentMenuPage  = ((currentMenuPage-1)>=0) ? currentMenuPage-1 : 3;
            onMenuPageChangeCallBack.Invoke(currentMenuPage);
            Debug.Log(currentMenuPage);
        }
    }

    void CheckOpenMenu(int newMenuPage){

    }



    
}
