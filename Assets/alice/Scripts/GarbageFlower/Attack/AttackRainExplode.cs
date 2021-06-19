using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class AttackRainExplode : CAttack
{
    [SerializeField] private float BornTime = 5f;
    [SerializeField] private float ForwardSpeed = 10f;
    [SerializeField] private float StartHeight = 5f;
    [SerializeField] private float AttackRange = 15f;
    [SerializeField] private float ClearRange = 3f;
    [SerializeField] private Vector3 _targetLocation = Vector3.zero;
    [Range(1.0f,10.0f),SerializeField] private float _fMoveDuration = 1.0f;//動畫播ㄉ時間
    [SerializeField] private Ease _moveEase = Ease.Linear;//線性變化

    private float _fAttackRangeX = 0f;
    private float _fAttackRangeZ = 0f;
    

    

    
    public override void OnObjectSpawn(){
        _fBorntimer = 0f;
        _fBornTime = BornTime;
    }
    void Update(){
        _fBorntimer += Time.deltaTime;
        SetAttackInvisible(ref _fBorntimer,_fBornTime);
    }
    

}