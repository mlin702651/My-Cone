using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestRewardManager : MonoBehaviour
{
    public static QuestRewardManager instance;
    private void Awake(){
        if(instance != null){
            //Debug.LogWarning("fix this: " + gameObject.name);
            Destroy(gameObject);
        }
        else instance = this;
    }
    
    public GameObject questRewardUI; 
    public Text questName;
    public Text questRewardNames;

    [SerializeField]private float questRewardUIShowDuration = 2.0f;
    private bool ifQuestRewardStart = false;
    private float questRewardTimer = 0;

    public void SetRewardUI(QuestBase questBase){
        
        ifQuestRewardStart = true;
        questRewardUI.SetActive(true); //這邊記得改成dotween animation
        
        questName.text = questBase.questName;
        if(questBase.rewards.rewardProps.Length!=0){
            questRewardNames.text = "獲得 : ";
            for(int i=0; i < questBase.rewards.rewardProps.Length ; i++){ //
                questRewardNames.text += questBase.rewards.rewardProps[i].itemName + " ";
                InventoryManager.instance.AddItem(questBase.rewards.rewardProps[i]);
            }
        }
        else{
            questRewardNames.text = "";
        }
        Debug.Log("questReward = " + questRewardNames.text);

        //這邊要寫給玩家獎勵的程式碼 we need an InventoryManager!!

    }
    private void Update() {
        if(ifQuestRewardStart){
            questRewardTimer+= Time.deltaTime;
        }
        if(questRewardTimer>= questRewardUIShowDuration){
            questRewardUI.SetActive(false);
            questRewardTimer = 0;
            ifQuestRewardStart = false;
        }
    }
}
