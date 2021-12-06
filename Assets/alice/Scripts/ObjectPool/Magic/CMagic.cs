using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMagic : MonoBehaviour,IPooledObject
{
    protected float _fBorntimer;
    protected float _fBornTime;
    protected float _fForwardSpeed ;
    protected ObjectPooler testPooler;

    public virtual void OnObjectSpawn(){

    }
    public virtual void SetAttackInvisible(ref float _fBorntr,float _fBornt){
        if(_fBorntr>=_fBornt){
            //print("3sec!!!!");
            _fBorntr = 0;
            gameObject.SetActive(false);
        }
    }
    public virtual void OnCollisionEnter(Collision other) {
            //attackPooler.SpawnFromPool("RainExplode",transform.position,transform.eulerAngles);
        
    }
}
