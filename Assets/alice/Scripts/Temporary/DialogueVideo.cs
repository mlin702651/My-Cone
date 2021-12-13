using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Video Dialogue")]
public class DialogueVideo : DialogueBase
{
   public void StartEvent(){
       TrainingThreeEndingEvent.instance.StartVideo();
   }
}
