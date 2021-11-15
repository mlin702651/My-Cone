using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInitializedTriggerDestroy : CQuestInitializedTrigger
{
    public override void QuestTriggerEvent(){
        
        Destroy(gameObject);
        
    }
}
