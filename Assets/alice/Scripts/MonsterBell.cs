using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MonsterBell : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject leaf;
    [SerializeField]private float floatingDuration;
    [SerializeField]private float floatingPosition;
    Sequence sequence = DOTween.Sequence();
    void Start()
    {
         transform.DOMoveY(floatingPosition, floatingDuration).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
       
        
       
    }
}
