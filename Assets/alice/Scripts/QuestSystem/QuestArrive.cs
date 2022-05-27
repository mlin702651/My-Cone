using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrive Quest", menuName = "Quests/Arrive Quest")]
public class QuestArrive : QuestBase
{
   [System.Serializable]
    public class Objective{
        public string sceneName;
        public string sceneShowName;
        public int requiredAmount;
    }
    public Objective[] objectives;

    public override void InitializedQuest(){

        RequiredAmount = new int[objectives.Length];

        for(int i=0;i< objectives.Length;i++){
            RequiredAmount[i] = objectives[i].requiredAmount;
        }

        GameManager.instance.onPlayerArrivedCallBack += ArriveGoal;
        base.InitializedQuest();
    }

    public override void Evaluate(){
        if(CurrentAmount[0] < RequiredAmount[0]){
            return;//只要有人沒達成就結束(退回?)
        }
        Debug.Log("Quest is complete!");
        GameManager.instance.PlayAudio();

        DialogueManager.instance.CompletedQuest = this;
        Debug.Log("Arrived.");
        GameManager.instance.onPlayerArrivedCallBack -= ArriveGoal;
        QuestMenuManager.instance.RemoveQuestFromList(this);
    }


    private void ArriveGoal(string newSceneName){
        Debug.Log("Arrive?");
        for(int i = 0; i < objectives.Length; i++){
            if(newSceneName == objectives[i].sceneName){
                CurrentAmount[i]++;
                //更新給UI
                QuestManager.instance.UpdateQuestTracker(this.GetObjectiveList());
                Debug.Log("Player arrived" + objectives[i].sceneShowName);
                questStatus.IsCompleted = true;
            }
        }
        
        Evaluate();
    }

    public override string GetObjectiveList(){
        
        string tempObjectiveList = "";

        for (int i = 0; i < objectives.Length; i++)
        {
            tempObjectiveList += $"前往{"" + objectives[i].sceneShowName + "     (" + CurrentAmount[i] + " / " + RequiredAmount[i]+ " )"} \n";
        }

        return tempObjectiveList;
    }
}
