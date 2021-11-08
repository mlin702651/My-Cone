using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQuestInitializedTrigger : MonoBehaviour
{
    [SerializeField]private QuestBase InitialedQuest;
    private bool isTrigger = false;
    
    void Update()
    {
        if(isTrigger) return;
        if(InitialedQuest.questStatus.IsInitialized== true){
            isTrigger = true;
            QuestTriggerEvent();
            
        }
    }

    public virtual void QuestTriggerEvent(){

    }
}
