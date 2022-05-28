using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collect Quest", menuName = "Quests/Collect Quest")]
public class QuestCollect : QuestBase
{
    [System.Serializable]
    public class Objective{
        public QuestProp requiredQuestProp;
        public int requiredAmount;
    }
    public Objective[] objectives;

    public override void InitializedQuest(){

        RequiredAmount = new int[objectives.Length];

        for(int i=0;i< objectives.Length;i++){
            RequiredAmount[i] = objectives[i].requiredAmount;
        }

        GameManager.instance.onPlayerCollectCallBack += ItemCollect;
        base.InitializedQuest();
    }

    private void ItemCollect(QuestProp questProp){
        for(int i = 0; i < objectives.Length; i++){
            if(questProp == objectives[i].requiredQuestProp){
                CurrentAmount[i]++;
                //更新給UI
                //QuestManager.instance.UpdateQuestTracker($"{questDescription + "    " + CurrentAmount[i] + "/" + RequiredAmount[i]}");
                QuestManager.instance.UpdateQuestTracker(this.GetObjectiveList());
            }
        }

        Evaluate();
    }

    public override void Evaluate()
    {
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
        GameManager.instance.onPlayerCollectCallBack -= ItemCollect;
    }

    public override string GetObjectiveList(){
        
        string tempObjectiveList = "";

        for (int i = 0; i < objectives.Length; i++)
        {
            tempObjectiveList += $"已收集{"    ( " + CurrentAmount[i] + " / " + RequiredAmount[i]+ " ) " + objectives[i].requiredQuestProp.itemName} \n";
        }

        return tempObjectiveList;
    }
}
