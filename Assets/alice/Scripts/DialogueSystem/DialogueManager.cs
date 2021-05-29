using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour {
    
    //客製化的部分
    public Text nameText;
    public Text dialogueText;
    public Image thumbnailImage;

    public RectTransform dialogueSprite;

    [SerializeField]public Ease DialogueEaseIn;
    [SerializeField]public float DialogueInTime;
    [SerializeField]public Ease DialogueEaseOut;
    [SerializeField]public float DialogueOutTime;

    [SerializeField]public Sprite Woomi;//阿這個是不是可以用dictionary???

    [System.Serializable] public class CharacterInfo{
        public string characterName;
        public Sprite thumbnail;
    }

    public List<CharacterInfo> characterInfo;
    //private
    Controls controls;
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<int> identities;
    private bool canPlayNext = true;
    private Dictionary<int , Sprite> thumbnailDictionary;
    private Dictionary<int , string> nameDictionary;


    private string sentence;
    private int identity;
    private float timer = 0;

    void FinishDialogue(){
        canPlayNext = true;
        print("test");
    }
    
    void Start(){
        sentences = new Queue<string>();
        names = new Queue<string>();
        identities = new Queue<int>();
        nameDictionary = new Dictionary<int,string>();
        thumbnailDictionary = new Dictionary<int , Sprite>();
        int i=0;
        foreach(CharacterInfo info in characterInfo){
            nameDictionary.Add(i,info.characterName);
            thumbnailDictionary.Add(i,info.thumbnail);
            i++;
        }
        
    }

    

    public void StartDialogue(Dialogue dialogue){

        Debug.Log("Starting conversation with" + dialogue.dialogueSet[0].name);
        nameText.text = dialogue.dialogueSet[0].name;//設定名字


        sentences.Clear(); //把之前拿到的清除
        names.Clear(); //把之前拿到的清除

        foreach(DialogueSet dl in dialogue.dialogueSet){
            sentences.Enqueue(dl.sentences); //把新拿到的加進去
            names.Enqueue(dl.name); //把新拿到的加進去
            identities.Enqueue((int)dl.chara); //把新拿到的加進去
            //identities.Enqueue(1); //把新拿到的加進去
        }

        DisplayNextSentence();

        //UI上升的動畫
        dialogueSprite.DOAnchorPosY(-365,DialogueInTime,true).SetEase(DialogueEaseIn);
        //讓主角不能動
        FindObjectOfType<SimpleMovement>().SetTalkingStatus(true);

    }

    public void DisplayNextSentence(){
        if(canPlayNext&&sentences.Count ==0){
            EndDialogue();
            return;
        }
        if(canPlayNext){

            //name = names.Dequeue();
            Debug.Log(names);
            nameText.text="";
            nameText.text = names.Dequeue();
            
            sentence = sentences.Dequeue();
            Debug.Log(sentence);
            dialogueText.text = "";
            dialogueText.DOText(sentence, sentence.Length*0.05f).SetRelative().SetEase(Ease.Linear).OnComplete(()=>FinishDialogue());
            
            identity = identities.Dequeue();
            if(!thumbnailDictionary.ContainsKey(identity)){
                Debug.LogWarning("thumbnail with tag" + tag + "doesn't exsist.");
                thumbnailImage.sprite = null;
            }
            else{
                thumbnailImage.sprite = thumbnailDictionary[identity];
            }

            canPlayNext = false;
        }
        
        
    }

    void EndDialogue(){
        Debug.Log("End of conversation");
        dialogueSprite.DOAnchorPosY(-850,DialogueOutTime,true).SetEase(DialogueEaseOut);
        FindObjectOfType<SimpleMovement>().SetTalkingStatus(false);
    }

}

//待修正 連按會讓對話壞掉 要設個bool控制可不可以講下一句話 
//要改成按上才會開始對話 現在一碰到就會強制講話 會逃不出