using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDialogue : MonoBehaviour
{
    public DialogueBase testtest;
    private void Start(){
       
    }

    private void OnTriggerEnter(Collider other) {
        DialogueManager.instance.EnqueueDialogue(testtest);
        DialogueManager.instance.StartDialogue();
    }
}
