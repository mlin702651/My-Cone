using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kill Quest", menuName = "Quests/Kill Quest")]
public class QuestKill : QuestBase
{
    [System.Serializable]
    public class Objective{
        public MonsterProfile requiredMonster;
        public int requiredAmount;
    }

    public Objective[] objectives;

    public override void InitializedQuest(){

        RequiredAmount = new int[objectives.Length];

        for(int i=0;i< objectives.Length;i++){
            RequiredAmount[i] = objectives[i].requiredAmount;
        }

        GameManager.instance.onEnemyDeathCallBack += MonsterDeath;
        base.InitializedQuest();
    }

    private void MonsterDeath(MonsterProfile slaintMonster){
        for(int i = 0; i < objectives.Length; i++){
            if(slaintMonster == objectives[i].requiredMonster){
                CurrentAmount[i]++;
            }
        }

        Evaluate();
    }

}
