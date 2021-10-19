﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQuestFinishedTrigger : MonoBehaviour
{
    [SerializeField]private QuestBase finishedQuest;
    private bool isTrigger = false;
    
    void Update()
    {
        if(isTrigger) return;
        if(finishedQuest.questStatus.IsCompleted== true){
            isTrigger = true;
            QuestTriggerEvent();
            
        }
    }

    public virtual void QuestTriggerEvent(){

    }
}