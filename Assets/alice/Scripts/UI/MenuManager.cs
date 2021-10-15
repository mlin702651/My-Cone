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
    public Text questReward;
    public Transform questHolder;
    public GameObject questSelectionPrefab;

    //update UI qhen open
    private QuestBase LastDisplayQuest;
    private int currentSelectedQuest = 0;

    //private QuestSelection[] questList;

    private void Update() {
        if(InputSystem.instance.MenuPressDown){
            InputSystem.instance.MenuPressDown = false;
            currentSelectedQuest = 0;
            MenuUI.SetActive(!MenuUI.activeSelf);
            
        }
        if(MenuUI.activeSelf){ //if menu is open
                
                var questListCount = questHolder.childCount;
                
                //print(questListCount);
                for (int i = 0; i < questListCount; i++)
                {
                    var selection = questHolder.GetChild(i).GetComponent<QuestSelection>();//我覺得這樣寫是效能小偷:()
                    selection.questBackground.color = Color.yellow;
                }
                if(InputSystem.instance.MenuSelectDownPressDown){
                    print("press down");
                    InputSystem.instance.MenuSelectDownPressDown = false;
                    currentSelectedQuest = currentSelectedQuest< (questListCount-1)? currentSelectedQuest+1 : currentSelectedQuest;
                    //currentSelectedQuest++;
                    print(currentSelectedQuest);
                }
                else if(InputSystem.instance.MenuSelectUpPressDown){
                    print("press up");
                    InputSystem.instance.MenuSelectUpPressDown = false;
                    currentSelectedQuest = currentSelectedQuest== 0? currentSelectedQuest : currentSelectedQuest-1;
                    print(currentSelectedQuest);

                }

                try{
                    var firstSelection = questHolder.GetChild(currentSelectedQuest).GetComponent<QuestSelection>();
                    //make it be select ex.change color?
                    firstSelection.questBackground.color = Color.gray;
                    FindObjectOfType<WoomiMovement>().SetInMenuStatus(true);
                    UpdateQuestUI(firstSelection.questBase,firstSelection.questBase.GetObjectiveList());
                }
                catch{
                    return;
                }

            }
            else FindObjectOfType<WoomiMovement>().SetInMenuStatus(false);
        
    }

    public void UpdateQuestUI(QuestBase newQuest, string objectiveList){
        LastDisplayQuest = newQuest;

        questName.text = newQuest.questName;
        questDetail.text = newQuest.questDetail;
        questDescription.text = newQuest.questDescription;
        questObjective.text = objectiveList;
        questReward.text = "";
        for (int i = 0; i < newQuest.rewards.itemRewardNames.Length; i++)
        {
            questReward.text += newQuest.rewards.itemRewardNames[i];
            questReward.text += "\n";
        }
        if(newQuest.rewards.experienceReward!=0)
            questReward.text = questReward.text + "經驗值 x " + newQuest.rewards.experienceReward.ToString() + "\n";
        if(newQuest.rewards.spiritReward!=0)
            questReward.text = questReward.text + "純淨靈魂 x " + newQuest.rewards.spiritReward.ToString() + "\n";
    }

    public void AddQuestToList(QuestBase newQuest){
        var questSelection = Instantiate(questSelectionPrefab, questHolder);

        questSelection.GetComponent<QuestSelection>().SetQuest(newQuest);
        UpdateQuestUI(newQuest,newQuest.GetObjectiveList());
        
        //questSelectionPrefab.Text.text = newQuest.questName;
        //questSelectionPrefab.GetComponent<Text>().text = newQuest.questName;
    }
}
