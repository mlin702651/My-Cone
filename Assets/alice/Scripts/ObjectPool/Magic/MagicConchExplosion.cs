using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicConchExplosion : CMagic
{
    [SerializeField] private float BornTime = 0.5f;//改3s
    
    
    public override void OnObjectSpawn(){
        _fBorntimer = 0f;
        _fBornTime = BornTime;
        //_fForwardSpeed = ForwardSpeed;
        
    }
    void Update(){
        _fBorntimer += Time.deltaTime;
        SetAttackInvisible(ref _fBorntimer,_fBornTime);
    }
}
