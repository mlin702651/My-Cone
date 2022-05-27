using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInitializedTriggerDisable : CQuestInitializedTrigger
{
    [SerializeField]private GameObject[] disableObjects;
    public override void QuestTriggerEvent(){
        
        foreach (var disableObject in disableObjects)
        {
            disableObject.SetActive(false);
        }
        
    }
}
