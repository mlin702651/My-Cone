using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicConchStart : CMagic
{
    [SerializeField] private float BornTime = 3f;//改3s
    
    
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
