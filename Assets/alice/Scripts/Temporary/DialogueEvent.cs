using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Event Dialogue")]
public class DialogueEvent : DialogueBase
{
   

   public void StartEvent(){
       TrainingThreeEndingEvent.instance.StartEvent();
   }

   
}
