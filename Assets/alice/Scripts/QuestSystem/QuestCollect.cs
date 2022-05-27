using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collect Quest", menuName = "Quests/Collect Quest")]
public class QuestCollect : QuestBase
{
    [System.Serializable]
    public class Objective{
        public CollectableProfile requiredCollectable;
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

    private void ItemCollect(CollectableProfile collectable){
        for(int i = 0; i < objectives.Length; i++){
            if(collectable == objectives[i].requiredCollectable){
                CurrentAmount[i]++;
                //更新給UI
                //QuestManager.instance.UpdateQuestTracker($"{questDescription + "    " + CurrentAmount[i] + "/" + RequiredAmount[i]}");
                QuestManager.instance.UpdateQuestTracker(this.GetObjectiveList());
            }
        }

        Evaluate();
    }

    public override string GetObjectiveList(){
        
        string tempObjectiveList = "";

        for (int i = 0; i < objectives.Length; i++)
        {
            tempObjectiveList += $"已收集{"    ( " + CurrentAmount[i] + " / " + RequiredAmount[i]+ " ) 隻" + objectives[i].requiredCollectable.collectableName} \n";
        }

        return tempObjectiveList;
    }
}
