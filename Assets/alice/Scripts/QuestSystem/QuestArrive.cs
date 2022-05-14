using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrive Quest", menuName = "Quests/Arrive Quest")]
public class QuestArrive : QuestBase
{
   [System.Serializable]
    public class Objective{
        public Object scene;
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
        base.Evaluate();
        GameManager.instance.onPlayerArrivedCallBack -= ArriveGoal;
    }


    private void ArriveGoal(string newSceneName){
        for(int i = 0; i < objectives.Length; i++){
            if(newSceneName == objectives[i].scene.name){
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
