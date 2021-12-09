﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBomb : CMagic
{
    [SerializeField] private float BornTime = 4f;//改3s
    private bool _isSmoke = false;
    
    public override void OnObjectSpawn(){
        _fBorntimer = 0f;
        _fBornTime = BornTime;
        _isSmoke = false;
        
    }
    void Update(){
        _fBorntimer += Time.deltaTime;
        SetAttackInvisible(ref _fBorntimer,_fBornTime);

        if(_fBorntimer>=3.2f && !_isSmoke){
            ObjectPooler.Instance.SpawnFromPool("MagicBombSmoke",transform.position,transform.eulerAngles);
            _isSmoke = true;
        }
    }
}