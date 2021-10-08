using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSelection : MonoBehaviour
{
    [HideInInspector]public QuestBase questBase;
    public Text questNameText;

    public void SetQuest(QuestBase newQuest){
        questBase = newQuest;
        questNameText.text = newQuest.questName;
    }
}
