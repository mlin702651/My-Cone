using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : ScriptableObject
{
    public string questName;
    [TextArea(2,10)]
    public string questDescription;
    [TextArea(5,10)]
    public string questDetail;
    [TextArea(2,10)]
    public string questExtraDetail;

    public int[] CurrentAmount {get; set;}
    public int[] RequiredAmount {get; set;}

    // [System.Serializable]
    // public class QuestStatus{
    //     public bool IsCompleted {get;set;} = false;
    // }
    public CQuestStatus questStatus;
    //public bool IsCompleted = false;
    public CharacterProfile NPCTurnIn;
    public DialogueBase completedQuestDialogue;
    [System.Serializable]
    public class Rewards{

        public string[] itemRewardNames; //可以自己定義物品 做成scriptable object
        public int experienceReward;
        public int spiritReward;
    }

    public Rewards rewards;
    public QuestBase(){
        questStatus = new CQuestStatus();
    }
    
    public virtual void InitializedQuest() {
        //questStatus.IsCompleted = false;
        CurrentAmount = new int[RequiredAmount.Length];
        QuestMenuManager.instance.AddQuestToList(this);
        questStatus.IsInitialized = true;
    }
    public virtual void Evaluate(){
        for(int i = 0; i < RequiredAmount.Length; i++){
            if(CurrentAmount[i] < RequiredAmount[i]){
                return;//只要有人沒達成就結束(退回?)
            }
        }
        Debug.Log("Quest is complete!");
        GameManager.instance.PlayAudio();

        for(int i=0; i < GameManager.instance.allDialogueTriggers.Count; i++){

            if(GameManager.instance.allDialogueTriggers[i].targetNPC == NPCTurnIn){

                GameManager.instance.allDialogueTriggers[i].HasCompletedQuest = true;
                GameManager.instance.allDialogueTriggers[i].CompletedQuestDialogue = completedQuestDialogue;
                break;
            }
        }
        //IsCompleted = true;
        DialogueManager.instance.CompletedQuest = this;
    }

    public virtual string GetObjectiveList(){
        return null;
    }
}
