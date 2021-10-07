using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialogueTrigger : DialogueTrigger
{
    [Header("Quest Dialogue Info")]
    [SerializeField] public bool hasActiveQuest;
    [SerializeField] private DialogueQuest[] dialogueQuests;
    public int QuestIndex {get; set;}

    public override void Interact()
    {
        if(hasActiveQuest){
            DialogueManager.instance.EnqueueDialogue(dialogueQuests[QuestIndex]);
            DialogueManager.instance.StartDialogue();
            QuestManager.instance.CurrentQuestDialogueTrigger = this;
        }    
        else{
            base.Interact();
        }
    }

}
