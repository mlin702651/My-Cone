using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int maxStackSize = 1;

    public Sprite itemIcon;
    //public GameObject itemPrefab;
}
