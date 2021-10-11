using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    private void Awake(){
        if(instance!= null){
             Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;
    }
    public GameObject MenuUI;
    public Text questName;
    public Text questDescription;
    public Text questDetail;
    public Text questObjective;
    public Transform questHolder;
    public GameObject questSelectionPrefab;

    private QuestBase TestQuest;

    private void Update() {
        if(InputSystem.instance.MenuPressDown){
            InputSystem.instance.MenuPressDown = false;
            MenuUI.SetActive(!MenuUI.activeSelf);
            if(MenuUI.activeSelf){
                FindObjectOfType<WoomiMovement>().SetInMenuStatus(true);
                UpdateQuestUI(TestQuest,TestQuest.GetObjectiveList());

            }
            else FindObjectOfType<WoomiMovement>().SetInMenuStatus(false);
        }
        
    }

    public void UpdateQuestUI(QuestBase newQuest, string objectiveList){
        questName.text = newQuest.questName;
        questDetail.text = newQuest.questDetail;
        questDescription.text = newQuest.questDescription;
        questObjective.text = objectiveList;
    }

    public void AddQuestToList(QuestBase newQuest){
        var questSelection = Instantiate(questSelectionPrefab, questHolder);

        questSelection.GetComponent<QuestSelection>().SetQuest(newQuest);
        UpdateQuestUI(newQuest,newQuest.GetObjectiveList());
        TestQuest = newQuest;
        //questSelectionPrefab.Text.text = newQuest.questName;
        //questSelectionPrefab.GetComponent<Text>().text = newQuest.questName;
    }
}
