using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinishedTriggerDisable : CQuestFinishedTrigger
{
    [SerializeField] private GameObject[] disableObjects;
    
    public override void QuestTriggerEvent(){
        print("disable");
        foreach (GameObject disableObject in disableObjects)
        {
            disableObject.SetActive(false);
        }
        
        
    }
}
