using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuManager : MonoBehaviour
{
    public static QuestMenuManager instance;
    private void Awake(){
        if(instance!= null){
             //Debug.LogWarning("fix this: " + gameObject.name);
            Destroy(gameObject);
        }
        else instance = this;
    }
    public GameObject QuestMenuUI;
    public Text questName;
    //public Text questDescription;
    public Text questDetail;
    public Text questExtraDetail;
    public Text questClient;
    public Text questObjective;
    //public Text questReward;
    public Transform questHolder;
    public GameObject questSelectionPrefab;

    [System.Serializable]
    private class QuestChild{
        public QuestBase childQuest;
        public GameObject childObj;
    }

    private List<QuestChild> questChildren = new List<QuestChild>();

    public bool inMenu;

    [SerializeField]private Color transparentColor;
    [SerializeField]private Color hoverColor;

    //update UI qhen open
    private QuestBase LastDisplayQuest;
    private int currentSelectedQuest = 0;

    private QuestChild tempChild;

    
    private void Start() {
        InventoryManager.instance.onMenuPageChangeCallBack += CheckOpenMenu; 
    }
    //private QuestSelection[] questList;

    private void Update() {
        
        
        
        // if(InputSystem.instance.MenuPressDown){
        //     InputSystem.instance.MenuPressDown = false;
        //     currentSelectedQuest = 0;
        //     QuestMenuUI.SetActive(!QuestMenuUI.activeSelf);
        //     Time.timeScale = QuestMenuUI.activeSelf? 0f:1f;
        //     inMenu = QuestMenuUI.activeSelf;
        // }
        if(QuestMenuUI.activeSelf){ //if menu is open
                
                var questListCount = questHolder.childCount;
                
                //print(questListCount);
                for (int i = 0; i < questListCount; i++)
                {
                    var selection = questHolder.GetChild(i).GetComponent<QuestSelection>();//我覺得這樣寫是效能小偷:()
                    selection.questBackground.color = transparentColor;
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
                    firstSelection.questBackground.color = hoverColor;
                    //FindObjectOfType<WoomiMovement>().SetInMenuStatus(true);
                    UpdateQuestUI(firstSelection.questBase,firstSelection.questBase.GetObjectiveList());
                }
                catch{
                    return;
                }

            }
            //else FindObjectOfType<WoomiMovement>().SetInMenuStatus(false);
        
    }

    public void UpdateQuestUI(QuestBase newQuest, string objectiveList){
        LastDisplayQuest = newQuest;

        questName.text = newQuest.questName;
        questDetail.text = newQuest.questDetail;
        questExtraDetail.text = newQuest.questExtraDetail;
        //questDescription.text = newQuest.questDescription;
        questObjective.text = objectiveList;
        questClient.text = "By. " + newQuest.NPCTurnIn.characterName;
        // questReward.text = "";
        // for (int i = 0; i < newQuest.rewards.itemRewardNames.Length; i++)
        // {
        //     questReward.text += newQuest.rewards.itemRewardNames[i];
        //     questReward.text += "\n";
        // }
        // if(newQuest.rewards.experienceReward!=0)
        //     questReward.text = questReward.text + "經驗值 x " + newQuest.rewards.experienceReward.ToString() + "\n";
        // if(newQuest.rewards.spiritReward!=0)
        //     questReward.text = questReward.text + "純淨靈魂 x " + newQuest.rewards.spiritReward.ToString() + "\n";
    }

    public void AddQuestToList(QuestBase newQuest){
        var questSelection = Instantiate(questSelectionPrefab, questHolder);

        questSelection.GetComponent<QuestSelection>().SetQuest(newQuest);
        UpdateQuestUI(newQuest,newQuest.GetObjectiveList());

        QuestChild newChild = new QuestChild();
        newChild.childQuest = newQuest;
        newChild.childObj = questSelection;
        questChildren.Add(newChild);
        
        //questSelectionPrefab.Text.text = newQuest.questName;
        //questSelectionPrefab.GetComponent<Text>().text = newQuest.questName;
    }

    public void RemoveQuestFromList(QuestBase questToRemove){
        if(questChildren==null) return;
        print(questChildren);
        foreach (var child in questChildren)
        {
            if(child.childQuest == questToRemove){
                tempChild = child;
            }
        }
        // if(questToRemove is QuestCollect){
        //     var collectQuest = questToRemove as QuestCollect;
        //     foreach (var objective in collectQuest.objectives)
        //     {
        //         for (int i = 0; i < objective.requiredAmount; i++)
        //         {
        //             InventoryManager.instance.RemoveItem(objective.requiredQuestProp);
        //         }
        //     }
        //     InventoryManager.instance.UpdateInventory();
        // }

        var tempObj = tempChild.childObj;
        questChildren.Remove(tempChild);
        Destroy(tempObj);

        questName.text = "任務列表";
        questDetail.text = "";
        questExtraDetail.text = "";
        questObjective.text = "";
        questClient.text = "";
    }

    public void CheckOpenMenu(int newMenuPage){
        if(newMenuPage == 1){
            QuestMenuUI.SetActive(true);
        }
        else QuestMenuUI.SetActive(false);
    }
}
