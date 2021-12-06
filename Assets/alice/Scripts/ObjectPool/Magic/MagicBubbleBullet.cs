using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBubbleBullet : CMagic
{
    [SerializeField] private float BornTime = 0.5f;
    [SerializeField] private float _bulletSpeed = 13.0f;
    private Rigidbody _rigidbody;
    
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public override void OnObjectSpawn(){
        _fBorntimer = 0f;
        _fBornTime = BornTime;
        //_fForwardSpeed = ForwardSpeed;
        
    }
    void Update(){
        _fBorntimer += Time.deltaTime;
        SetAttackInvisible(ref _fBorntimer,_fBornTime);
        _rigidbody.velocity = transform.TransformDirection(Vector3.forward * _bulletSpeed);//讓子彈飛
    }

    public override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player") return;
        ObjectPooler.Instance.SpawnFromPool("MagicBubbleBulletExplosion",transform.position,transform.eulerAngles);
        gameObject.SetActive(false);
    }
}
