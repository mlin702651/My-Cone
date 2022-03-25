using UnityEngine;

class AttackCube : CAttack {
    
    [SerializeField] private float BornTime = 3f;
    [SerializeField] private float ForwardSpeed = 10f;
    
    public override void OnObjectSpawn(){
        _fBorntimer = 0f;
        _fBornTime = BornTime;
        _fForwardSpeed = ForwardSpeed;
    }

    void Update(){
        _fBorntimer += Time.deltaTime;
        _fForwardSpeed += Time.deltaTime*3;
        transform.Translate(transform.forward * _fForwardSpeed * Time.deltaTime);
        SetAttackInvisible(ref _fBorntimer,_fBornTime);
    }
    
}