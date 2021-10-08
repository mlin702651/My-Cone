﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : ScriptableObject
{
    public string questName;
    [TextArea(2,10)]
    public string questDescription;
    [TextArea(5,10)]
    public string questDetail;

    public int[] CurrentAmount {get; set;}
    public int[] RequiredAmount {get; set;}

    public bool IsCompleted {get; set;}
    public CharacterProfile NPCTurnIn;
    public DialogueBase completedQuestDialogue;
    [System.Serializable]
    public class Rewards{

        public string[] itemRewardNames; //可以自己定義物品 做成scriptable object
        public int experienceReward;
        public int goldReward;
    }

    public Rewards rewards;
    public virtual void InitializedQuest() {
        CurrentAmount = new int[RequiredAmount.Length];
        MenuManager.instance.AddQuestToList(this);
    }
    public void Evaluate(){
        for(int i = 0; i < RequiredAmount.Length; i++){
            if(CurrentAmount[i] < RequiredAmount[i]){
                return;//只要有人沒達成就結束(退回?)
            }
        }
        Debug.Log("Quest is complete!");

        for(int i=0; i < GameManager.instance.allDialogueTriggers.Length; i++){

            if(GameManager.instance.allDialogueTriggers[i].targetNPC == NPCTurnIn){

                GameManager.instance.allDialogueTriggers[i].HasCompletedQuest = true;
                GameManager.instance.allDialogueTriggers[i].CompletedQuestDialogue = completedQuestDialogue;
                break;
            }
        }

        DialogueManager.instance.CompletedQuest = this;
    }
}
