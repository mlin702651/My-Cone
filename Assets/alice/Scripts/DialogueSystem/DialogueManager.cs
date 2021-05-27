using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour {
    
    //客製化的部分
    public Text nameText;
    public Text dialogueText;

    public RectTransform dialogueSprite;

    [SerializeField]public Ease DialogueEaseIn;
    [SerializeField]public float DialogueInTime;
    [SerializeField]public Ease DialogueEaseOut;
    [SerializeField]public float DialogueOutTime;

    [SerializeField]public Sprite Woomi;//阿這個是不是可以用dictionary???
    //private
    Controls controls;
    private Queue<string> sentences;


    
    void Start(){
        sentences = new Queue<string>();
        
    }

    

    public void StartDialogue(Dialogue dialogue){

        Debug.Log("Starting conversation with" + dialogue.name);
        nameText.text = dialogue.name;//設定名字


        sentences.Clear(); //把之前拿到的清除

        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence); //把新拿到的加進去
        }

        DisplayNextSentence();
        dialogueSprite.DOAnchorPosY(-365,DialogueInTime,true).SetEase(DialogueEaseIn);
        FindObjectOfType<SimpleMovement>().SetTalkingStatus(true);

    }

    public void DisplayNextSentence(){
        if(sentences.Count ==0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = "";
        dialogueText.DOText(sentence, sentence.Length*0.05f).SetRelative().SetEase(Ease.Linear);
        
    }

    void EndDialogue(){
        Debug.Log("End of conversation");
        dialogueSprite.DOAnchorPosY(-850,DialogueOutTime,true).SetEase(DialogueEaseOut);
        FindObjectOfType<SimpleMovement>().SetTalkingStatus(false);
    }

}

//待修正 連按會讓對話壞掉 要設個bool控制可不可以講下一句話 
//要改成按上才會開始對話 現在一碰到就會強制講話 會逃不出