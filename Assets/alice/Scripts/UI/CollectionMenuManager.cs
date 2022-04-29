using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionMenuManager : MonoBehaviour
{
    [SerializeField]public GameObject CollectionMenuUI;
    [SerializeField]private CollectionUI collectionUI;
    private int currentSelection = 0;
    private int newSelection = 0;
    private ItemBase currentSelectedItem;


    [SerializeField]private Image selectedItemImage;
    [SerializeField]private Text selectedItemName;
    [SerializeField]private Text selectedItemDescription;
    
    private void Start() {
        InventoryManager.instance.onMenuPageChangeCallBack += CheckOpenMenu; 
        InventoryManager.instance.onCollectionAddCallBack += AddItem;
        //CollectionMenuUI.SetActive(false);

    }
    private void Update() {
        if(CollectionMenuUI.activeSelf){
            HandleSelection();
        }
    }

    private void AddItem(ItemBase newItem){
        collectionUI.UpdateInventoryAdd(newItem);
    }
    
    void HandleSelection(){
        //if(InventoryManager.instance.currentMenuPage!=0) return;
        if(InventoryManager.instance.collection.Count <=0) return;
        
        if(InputSystem.instance.MenuSelectDownPressDown){ //按往下的鈕
            InputSystem.instance.MenuSelectDownPressDown = false;
            Debug.Log("down");
            newSelection = (currentSelection+3<InventoryManager.instance.collection.Count)?currentSelection+3:currentSelection; //如果有下一排 選下面那個
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
            newSelection = ((currentSelection+1)<InventoryManager.instance.collection.Count)?currentSelection+1:currentSelection; //如果有上一排 選上面那個
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
        if(InventoryManager.instance.collection.Count <=0) return;
        //onSelectionChangeCallBack.Invoke(newSelection,currentSelection);
        //currentSelectedItem = getCurrentItemCallBack.Invoke(newSelection);
        currentSelectedItem = InventoryManager.instance.collection[newSelection];
        selectedItemImage.overrideSprite = currentSelectedItem.itemIcon;
        selectedItemName.text = currentSelectedItem.itemName;
        selectedItemDescription.text = currentSelectedItem.itemDescription;
        print(newSelection + "/" +currentSelection);
        collectionUI.UpdateInventorySelection(newSelection,currentSelection);
        currentSelection = newSelection;
    }
    public void CheckOpenMenu(int newMenuPage){
        if(newMenuPage == 2){
            CollectionMenuUI.SetActive(true);
            if(InventoryManager.instance.collection.Count != 0){
                currentSelectedItem = InventoryManager.instance.collection[0];
                newSelection = 0;
                currentSelection = 0;
                UpdateSelection();
            }
        }
        else CollectionMenuUI.SetActive(false);
    }
}
