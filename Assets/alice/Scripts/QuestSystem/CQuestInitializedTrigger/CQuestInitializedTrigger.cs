using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQuestInitializedTrigger : MonoBehaviour
{
    [SerializeField]private QuestBase InitialedQuest;
    private bool isTrigger = false;
    
    void Start(){
        if(InitialedQuest.questStatus.IsInitialized== true){
            isTrigger = true;
            QuestTriggerEvent();
            
        }
    }
    void Update()
    {
        if(isTrigger) return;
        if(InitialedQuest.questStatus.IsInitialized== true){
            isTrigger = true;
            QuestTriggerEvent();
            
        }
    }

    public bool GetIsTrigger(){
        return isTrigger;
    }

    public virtual void QuestTriggerEvent(){

    }
}
