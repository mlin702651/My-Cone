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
    public Transform questHolder;
    public GameObject questSelectionPrefab;

    private QuestBase TestQuest;

    private void Update() {
        if(InputSystem.instance.MenuPressDown){
            InputSystem.instance.MenuPressDown = false;
            MenuUI.SetActive(!MenuUI.activeSelf);
            if(MenuUI.activeSelf){
                FindObjectOfType<WoomiMovement>().SetInMenuStatus(true);
                UpdateQuestUI(TestQuest);

            }
            else FindObjectOfType<WoomiMovement>().SetInMenuStatus(false);
        }
        
    }

    public void UpdateQuestUI(QuestBase newQuest){
        questName.text = newQuest.questName;
        questDetail.text = newQuest.questDetail;
        questDescription.text = newQuest.questDescription + "    ( " + newQuest.CurrentAmount[0] + " / " + newQuest.RequiredAmount[0] + " )";
    }

    public void AddQuestToList(QuestBase newQuest){
        var questSelection = Instantiate(questSelectionPrefab, questHolder);

        questSelection.GetComponent<QuestSelection>().SetQuest(newQuest);
        UpdateQuestUI(newQuest);
        TestQuest = newQuest;
        //questSelectionPrefab.Text.text = newQuest.questName;
        //questSelectionPrefab.GetComponent<Text>().text = newQuest.questName;
    }
}
