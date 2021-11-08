using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInitializedTriggerEnable : CQuestInitializedTrigger
{
    [SerializeField]private GameObject[] enableObjects;
    public override void QuestTriggerEvent(){
        
        foreach(GameObject enableObject in enableObjects){
            enableObject.SetActive(true);
        }
        
    }
}
