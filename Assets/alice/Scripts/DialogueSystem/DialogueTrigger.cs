using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]private Image dialogueHint;
    public Dialogue dialogue;
    private void Start() {
        dialogueHint.DOFade(0,0f);
    }
    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    // private void OnCollisionEnter(Collision other) {
        
    //     //TriggerDialogue();
    // }
    // private void OnCollisionExit(Collision other) {
        
    // }

    private void OnTriggerEnter(Collider other) {
        dialogueHint.DOFade(1,0.5f);
        FindObjectOfType<SimpleMovement>().SetCanTalkStatus(true);
        FindObjectOfType<SimpleMovement>().GetDialogue(dialogue);
    }
    
    private void OnTriggerExit(Collider other) {
        dialogueHint.DOFade(0,0.3f);
        FindObjectOfType<SimpleMovement>().SetCanTalkStatus(false);
    }
}
