using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInitializedTriggerDestroyWhenStart : CQuestInitializedTrigger
{
    public override void Update()
    {
        
    }
    public override void QuestTriggerEvent(){
        
        Destroy(gameObject);
        
    }
}
