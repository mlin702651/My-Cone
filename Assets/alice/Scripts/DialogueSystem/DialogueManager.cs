using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour {
    
    #region old dialogue manager
    
    // //客製化的部分
    // public Text nameText;
    // public Text dialogueText;
    // public Image thumbnailImage;

    // public RectTransform dialogueSprite;

    // [SerializeField]public Ease DialogueEaseIn;
    // [SerializeField]public float DialogueInTime;
    // [SerializeField]public Ease DialogueEaseOut;
    // [SerializeField]public float DialogueOutTime;

    // [SerializeField]public Sprite Woomi;//阿這個是不是可以用dictionary???

    // [System.Serializable] public class CharacterInfo{
    //     public string characterName;
    //     public Sprite thumbnail;
    // }

    // public List<CharacterInfo> characterInfo;
    // //private
    // Controls controls;
    // private Queue<string> sentences;
    // private Queue<string> names;
    // private Queue<int> identities;
    // private bool canPlayNext = true;
    // private Dictionary<int , Sprite> thumbnailDictionary;
    // private Dictionary<int , string> nameDictionary;


    // private string sentence;
    // private int identity;
    // private float timer = 0;

    // void FinishDialogue(){
    //     canPlayNext = true;
    //     print("test");
    // }
    
    // void Start(){
    //     sentences = new Queue<string>();
    //     names = new Queue<string>();
    //     identities = new Queue<int>();
    //     nameDictionary = new Dictionary<int,string>();
    //     thumbnailDictionary = new Dictionary<int , Sprite>();
    //     int i=0;
    //     foreach(CharacterInfo info in characterInfo){
    //         nameDictionary.Add(i,info.characterName);
    //         thumbnailDictionary.Add(i,info.thumbnail);
    //         i++;
    //     }
        
    // }

    

    // public void StartDialogue(Dialogue dialogue){

    //     Debug.Log("Starting conversation with" + dialogue.dialogueSet[0].name);
    //     nameText.text = dialogue.dialogueSet[0].name;//設定名字


    //     sentences.Clear(); //把之前拿到的清除
    //     names.Clear(); //把之前拿到的清除

    //     foreach(DialogueSet dl in dialogue.dialogueSet){
    //         sentences.Enqueue(dl.sentences); //把新拿到的加進去
    //         names.Enqueue(dl.name); //把新拿到的加進去
    //         identities.Enqueue((int)dl.chara); //把新拿到的加進去
    //         //identities.Enqueue(1); //把新拿到的加進去
    //     }

    //     DisplayNextSentence();

    //     //UI上升的動畫
    //     dialogueSprite.DOAnchorPosY(-365,DialogueInTime,true).SetEase(DialogueEaseIn);
    //     //讓主角不能動
    //     FindObjectOfType<WoomiMovement>().SetTalkingStatus(true);

    // }

    // public void DisplayNextSentence(){
    //     if(canPlayNext&&sentences.Count ==0){
    //         EndDialogue();
    //         return;
    //     }
    //     if(canPlayNext){

    //         //name = names.Dequeue();
    //         Debug.Log(names);
    //         nameText.text="";
    //         nameText.text = names.Dequeue();
            
    //         sentence = sentences.Dequeue();
    //         Debug.Log(sentence);
    //         dialogueText.text = "";
    //         dialogueText.DOText(sentence, sentence.Length*0.05f).SetRelative().SetEase(Ease.Linear).OnComplete(()=>FinishDialogue());
            
    //         identity = identities.Dequeue();
    //         if(!thumbnailDictionary.ContainsKey(identity)){
    //             Debug.LogWarning("thumbnail with tag" + tag + "doesn't exsist.");
    //             thumbnailImage.sprite = null;
    //         }
    //         else{
    //             thumbnailImage.sprite = thumbnailDictionary[identity];
    //         }

    //         canPlayNext = false;
    //     }
        
        
    // }

    // void EndDialogue(){
    //     Debug.Log("End of conversation");
    //     dialogueSprite.DOAnchorPosY(-850,DialogueOutTime,true).SetEase(DialogueEaseOut);
    //     FindObjectOfType<WoomiMovement>().SetTalkingStatus(false);
    // }

    #endregion

    public static DialogueManager instance;
    private void Awake(){
        if(instance != null){
            Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;
    }
    [Header("Dialogue Box")]
    public RectTransform dialogueBox;
    public Text dialogueName;
    public Text dialogueContent;
    public Image dialogueImage;

    [SerializeField]public Ease DialogueEaseIn;
    [SerializeField]public float DialogueInTime;
    [SerializeField]public Ease DialogueEaseOut;
    [SerializeField]public float DialogueOutTime;

    [Header("Variable")]
    public float typeAnimationDelay = 0.001f;
    private bool isTyping = false;
    private string completeContent;
    public bool inDialogue = false;

    [Header("Quest")]
    private DialogueBase currentDialogue;

    public Queue<DialogueBase.DialogueSet> dialogueSets = new Queue<DialogueBase.DialogueSet>();

    public QuestBase CompletedQuest {get; set;}
    public bool CompletedQuestReady {get; set;}
    public void EnqueueDialogue(DialogueBase dialogueBase){ //把一個一個dialogueSet加進queue裡面
        dialogueSets.Clear();
        currentDialogue = dialogueBase;
        inDialogue = true;

        foreach(DialogueBase.DialogueSet dialogueSet in dialogueBase.dialogueSet){
            dialogueSets.Enqueue(dialogueSet);
        }
    }

    public void DequeueDialogue(){
        if(isTyping){
            CompleteContent();
            StopAllCoroutines();
            isTyping = false;
            return;
        }
        if(dialogueSets.Count == 0){
            inDialogue = false;
            
            EndDialogue();
            return;
        }

        DialogueBase.DialogueSet dialogueSet = dialogueSets.Dequeue();
        completeContent = dialogueSet.content;

        dialogueName.text = dialogueSet.character.characterName;
        dialogueContent.text = dialogueSet.content;
        switch(dialogueSet.emotion){
            case DialogueBase.DialogueSet.Emotions.Normal:
                dialogueImage.sprite = dialogueSet.character.characterImage_Normal;
                break;
            case DialogueBase.DialogueSet.Emotions.Happy:
                dialogueImage.sprite = dialogueSet.character.characterImage_Happy;
                break;
            case DialogueBase.DialogueSet.Emotions.Mad:
                dialogueImage.sprite = dialogueSet.character.characterImage_Mad;
                break;
            case DialogueBase.DialogueSet.Emotions.Ok:
                dialogueImage.sprite = dialogueSet.character.characterImage_Ok;
                break;
            case DialogueBase.DialogueSet.Emotions.Scare:
                dialogueImage.sprite = dialogueSet.character.characterImage_Scare;
                break;
            case DialogueBase.DialogueSet.Emotions.Shock:
                dialogueImage.sprite = dialogueSet.character.characterImage_Shock;
                break;
        }
                
        if(dialogueImage.sprite==null) dialogueImage.sprite = dialogueSet.character.characterImage_Normal;

        dialogueContent.text = "";
        StartCoroutine(TypeText(dialogueSet));
    }

    IEnumerator TypeText(DialogueBase.DialogueSet dialogueSet){
        isTyping = true;
        foreach(char c in dialogueSet.content.ToCharArray()){
            yield return new WaitForSeconds(typeAnimationDelay);
            dialogueContent.text += c;
        }
        isTyping = false;
    }

    private void CompleteContent(){
        dialogueContent.text = completeContent;
    }

    private void CheckIfDialogueQuest(){ //可能可以寫成check dialogue type 如果我們有選項的話啦
        if(currentDialogue is DialogueQuest){
            DialogueQuest dialogueQuest = currentDialogue as DialogueQuest;
            QuestManager.instance.InitiateQuest(dialogueQuest.quest);
        }
    }
    #region dialogue UI animation

    public void StartDialogue(){
        //UI上升的動畫
        dialogueBox.DOAnchorPosY(-170,DialogueInTime,true).SetEase(DialogueEaseIn);
        //讓主角不能動
        //FindObjectOfType<WoomiMovement>().SetTalkingStatus(true);
        DequeueDialogue();//輸出第一句話
    }
    public void EndDialogue(){
        CheckIfDialogueQuest();
        SetRewards();
        Debug.Log("End of conversation");
        dialogueBox.DOAnchorPosY(-315,DialogueOutTime,true).SetEase(DialogueEaseOut);
        //FindObjectOfType<WoomiMovement>().SetTalkingStatus(false);
    }

    private void SetRewards(){
        if(CompletedQuestReady){
            //任務完成
            CompletedQuest.questStatus.IsCompleted = true;
            QuestRewardManager.instance.SetRewardUI(CompletedQuest);
            QuestManager.instance.ClearCompletedQuest();
            CompletedQuestReady = false;
        }
    }
    #endregion
}
