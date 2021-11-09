using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQuestStatus
{
   public CQuestStatus(){
       IsCompleted = false;
       IsInitialized = false;
   }
   public bool IsCompleted {get; set;}= false;
   public bool IsInitialized {get; set;}= false;
}
