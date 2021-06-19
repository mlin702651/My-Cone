using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class AttackRain : CAttack
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
        _fAttackRangeX = Random.Range(-AttackRange, AttackRange);
        _fAttackRangeZ = Random.Range(-AttackRange, AttackRange);
        transform.position = new Vector3(
            (_fAttackRangeX < 0 ? -_fAttackRangeX : _fAttackRangeX)>ClearRange ? _fAttackRangeX : ClearRange+_fAttackRangeX,
            Random.Range(StartHeight+1,StartHeight-1),
            (_fAttackRangeZ < 0 ? -_fAttackRangeZ : _fAttackRangeZ)>ClearRange ? _fAttackRangeZ : ClearRange+_fAttackRangeZ
        );
        //transform.DOMoveY(_targetLocation.y, _fMoveDuration).SetEase(_moveEase); 
        _fBorntimer = 0f;
        _fBornTime = BornTime;
        _fForwardSpeed = ForwardSpeed;
        
    }
    void Update(){
        _fBorntimer += Time.deltaTime;
        SetAttackInvisible(ref _fBorntimer,_fBornTime);
    }
    public override void OnCollisionEnter(Collision other) {
            Debug.Log("collide");
            testPooler.SpawnFromPool("RainExplode",transform.position,transform.eulerAngles);
            gameObject.SetActive(false);

        
    }

}