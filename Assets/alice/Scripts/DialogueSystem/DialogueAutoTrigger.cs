using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAutoTrigger : Interactable
{
    
    [SerializeField]private DialogueBase dialogueBase;

    #region old dialogue trigger
    
    #endregion
    public override void Start(){
        base.Start();
        
    }

    public override void Update()
    {
        if(canInteract){
            canInteract = false;
            Interact();
        }
    }
    public override void Interact(){
        
        DialogueManager.instance.EnqueueDialogue(dialogueBase);
        DialogueManager.instance.StartDialogue();

    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"){
            canInteract = true;
        }
    }
    public override void OnTriggerExit(Collider other)
    {
        canInteract = false;
    }
}
