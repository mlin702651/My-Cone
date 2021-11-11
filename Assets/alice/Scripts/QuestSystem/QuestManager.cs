using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    private void Awake(){
        if(instance!= null){
             Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;
    }

    private void Start() {
        questName.text = "";
        questDescription.text = "";
    }

    
    public GameObject questUI;
    public Text questName;
    public Text questDescription;

    public QuestBase CurrentQuest {get; set;}
    public QuestDialogueTrigger CurrentQuestDialogueTrigger{get; set;}

    public void InitiateQuest(QuestBase newQuest){
        
        newQuest.InitializedQuest();//直接接受ㄌ
        questName.text = newQuest.questName;
        //questDescription.text = newQuest.questDescription + "\n" + newQuest.GetObjectiveList();
        questDescription.text = newQuest.GetObjectiveList();
        
        CurrentQuest = newQuest;
        CurrentQuestDialogueTrigger.hasActiveQuest = false;

        Debug.Log("接受任務---" + questName.text + " : " + questDescription.text);
        //這邊在寫個任務UI跑進來淡出的動畫
    }
    public void UpdateQuestTracker(string newDescription){
        questDescription.text = CurrentQuest.questDescription + "\n" + newDescription;
    }

    public void ClearCompletedQuest(){
        //這個之後要改寫 現在只會顯示一個任務所以不會出錯
        questName.text = "";
        questDescription.text = "";
    }
}
