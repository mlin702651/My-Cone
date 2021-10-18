using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class QuestFinishedTriggerMove : CQuestFinishedTrigger
{
   [SerializeField]private GameObject moveTarget;
   [SerializeField]private float moveDuration = 0;
   [SerializeField]private Ease moveEase = Ease.Linear;
   public override void QuestTriggerEvent(){
       print("move");
       gameObject.transform.DOMove(moveTarget.transform.position,moveDuration).SetEase(moveEase);
   }
}
