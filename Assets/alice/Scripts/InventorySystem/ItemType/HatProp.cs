using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HatProp", menuName = "Items/HatProps")]
public class HatProp :ItemBase
{
   public GameObject itemPrefab;
   public UnityEvent itemEvent;
}
