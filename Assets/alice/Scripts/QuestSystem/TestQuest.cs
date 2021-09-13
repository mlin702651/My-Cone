using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuest : MonoBehaviour
{
    public QuestBase quest;

    private void Start(){
        quest.InitializedQuest();
    }
}
