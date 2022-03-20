using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class AttackCircle : CAttack
{
    
    [SerializeField] private float BornTime = 5f;//改3s
    [SerializeField] private float ForwardSpeed = 10f;
    [SerializeField] private Vector3 BornScale = new Vector3(1f,5f,1f);
    [SerializeField] private Vector3 Scale = new Vector3(0.01f,0f,0.01f);
    [SerializeField] private Vector3 FinalScale = new Vector3(5f,5f,5f);
    [SerializeField] private Ease Ease = Ease.Linear;
    public override void OnObjectSpawn(){
        transform.localScale = BornScale; 
        _fBorntimer = 0f;
        _fBornTime = BornTime;
        _fForwardSpeed = ForwardSpeed;
        transform.DOScale(FinalScale,BornTime).SetEase(Ease);
        
    }
    void Update(){
        _fBorntimer += Time.deltaTime;
        //transform.localScale += Scale;
        //改成sin 0-pi/2 3s
        SetAttackInvisible(ref _fBorntimer,_fBornTime);
    }

}