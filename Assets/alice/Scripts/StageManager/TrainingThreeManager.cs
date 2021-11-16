using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    [SerializeField]private GameObject counDownUI;
    [SerializeField]private Text countDownText;
    private float counDownNumber = 3;

    [SerializeField]private CollideInteract collideInteract;
    
    // Start is called before the first frame update
    void Start()
    {
        monsterGenerator.GenerateOneMonster();
        counDownUI.SetActive(false);
        countDownText.text = counDownNumber.ToString("F0");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(questInitializedTriggerEnable.GetIsTrigger()){
            canStartTraining = true;
            

        }
        if(collideInteract.isInteract){
            counDownUI.SetActive(true);
            counDownNumber-= Time.deltaTime;
            countDownText.text = counDownNumber.ToString("F0");

        }
        if(counDownNumber<=0){
            counDownUI.SetActive(false);
        }
    }

    public void CheckBornNewMonster(){
        MonsterKilledCount++;
        if(MonsterToKill>MonsterKilledCount) monsterGenerator.GenerateOneMonster();
    }
}
