using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinishedTriggerEnable : CQuestFinishedTrigger
{
    [SerializeField] private GameObject[] enableObjects;
    
    public override void QuestTriggerEvent(){
        print("destroy");
        foreach (GameObject enableObject in enableObjects)
        {
            enableObject.SetActive(true);
        }
        
        
    }
}
