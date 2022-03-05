using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemBase testItem;
    void Start()
    {
       

    }
    private void Update() {
        if(InputSystem.instance.IsMagicPlusStatusPressed){   
            InventoryManager.instance.AddItem(testItem);
            Debug.Log("123123");
            }
        if(InputSystem.instance.IsMagicMinusStatusPressed) InventoryManager.instance.RemoveItem(testItem);
    }

   

    
}
