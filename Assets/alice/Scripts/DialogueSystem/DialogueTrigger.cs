using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueTrigger : Interactable
{
    [Header("This NPC")]
    public CharacterProfile targetNPC;
    [Header("Basic Dialogue Info")]
    
    [SerializeField]protected DialogueBase[] dialogueBases;
    public int index = 0;
    public bool nextDialogueInteract;
    
    public bool HasCompletedQuest {get; set;}
    public DialogueBase CompletedQuestDialogue {get; set;}

    #region old dialogue trigger
    // [SerializeField]private Image dialogueHint;
    // public Dialogue dialogue;
    // private void Start() {
    //     dialogueHint.DOFade(0,0f);
    // }
    // public void TriggerDialogue(){
    //     FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    // }

    // // private void OnCollisionEnter(Collision other) {
        
    // //     //TriggerDialogue();
    // // }
    // // private void OnCollisionExit(Collision other) {
        
    // // }

    // private void OnTriggerEnter(Collider other) {
    //     dialogueHint.DOFade(1,0.5f);
    //     FindObjectOfType<WoomiMovement>().SetCanTalkStatus(true);
    //     FindObjectOfType<WoomiMovement>().GetDialogue(dialogue);
    // }
    
    // private void OnTriggerExit(Collider other) {
    //     dialogueHint.DOFade(0,0.3f);
    //     FindObjectOfType<WoomiMovement>().SetCanTalkStatus(false);
    // }
    #endregion
    public override void Start(){
        base.Start();
        if(nextDialogueInteract){
            index = -1;
        }
        else {
            index = 0;
        }
    }
    public override void Interact(){
        
        if(!DialogueManager.instance.inDialogue){
            if(HasCompletedQuest){
                DialogueManager.instance.EnqueueDialogue(CompletedQuestDialogue);
                DialogueManager.instance.StartDialogue();
                DialogueManager.instance.CompletedQuestReady = true;
                HasCompletedQuest = false;
                return;
            }
        }
        
        if(nextDialogueInteract && !DialogueManager.instance.inDialogue){
            
            if(index < dialogueBases.Length -1){
                index++;
            }
        }
        DialogueManager.instance.EnqueueDialogue(dialogueBases[index]);
        DialogueManager.instance.StartDialogue();

    }
}
