using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingThreeManager:MonoBehaviour
{
    
     public static TrainingThreeManager instance;

    private void Awake(){
        if(instance!= null){
             Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;
    }
     
     public MonsterGenerator monsterGenerator;

     public QuestInitializedTriggerEnable questInitializedTriggerEnable;

    private int MonsterToKill = 3;
    private int MonsterKilledCount = 0;

    public bool canStartTraining = false;
    
    // Start is called before the first frame update
    void Start()
    {
        monsterGenerator.GenerateOneMonster();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(questInitializedTriggerEnable.GetIsTrigger()){
            canStartTraining = true;
        }
    }

    public void CheckBornNewMonster(){
        MonsterKilledCount++;
        if(MonsterToKill>MonsterKilledCount) monsterGenerator.GenerateOneMonster();
    }
}
