using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : ScriptableObject
{
    public string questName;
    [TextArea(5,10)]
    public string questDescription;

    public int[] CurrentAmount {get; set;}
    public int[] RequiredAmount {get; set;}

    public bool IsCompleted {get; set;}
    public CharacterProfile NPCTurnIn;
    public DialogueBase completedQuestDialogue;
    public virtual void InitializedQuest() {
        CurrentAmount = new int[RequiredAmount.Length];
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
    }
}
