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

    
    public GameObject questUI;
    public Text questName;
    public Text questDescription;

    public QuestBase CurrentQuest {get; set;}
    public QuestDialogueTrigger CurrentQuestDialogueTrigger{get; set;}

    public void InitiateQuest(QuestBase newQuest){
        questName.text = newQuest.questName;
        questDescription.text = newQuest.questDescription;
        
        newQuest.InitializedQuest();//直接接受ㄌ
        CurrentQuest = newQuest;
        CurrentQuestDialogueTrigger.hasActiveQuest = false;

        Debug.Log("接受任務---" + questName.text + " : " + questDescription.text);
        //這邊在寫個任務UI跑進來淡出的動畫
    }
}
