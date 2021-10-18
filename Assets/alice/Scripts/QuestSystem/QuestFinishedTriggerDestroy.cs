using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinishedTriggerDestroy : CQuestFinishedTrigger
{
    public override void QuestTriggerEvent(){
        print("destroy");
        Destroy(gameObject);
    }
}
