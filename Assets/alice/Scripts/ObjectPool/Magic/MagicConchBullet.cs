using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicConchBullet : CMagic
{
    [SerializeField] private float BornTime = 0.2f;
    [SerializeField] private float _bulletSpeed = 10.0f;
    //private Rigidbody _rigidbody;
    
    private void Awake() {
        //_rigidbody = GetComponent<Rigidbody>();
    }
    public override void OnObjectSpawn(){
        _fBorntimer = 0f;
        _fBornTime = PlayerMagicController.instance.MagicConchBornTime;
        //_fForwardSpeed = ForwardSpeed;
        
    }
    void Update(){
        _fBorntimer += Time.deltaTime;
        SetAttackInvisible(ref _fBorntimer,_fBornTime);
        //_rigidbody.velocity = transform.TransformDirection(Vector3.forward * _bulletSpeed);//讓子彈飛
        transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);

    }

    public override void OnTriggerEnter(Collider other)
    {
        
        ObjectPooler.Instance.SpawnFromPool("MagicConchBulletExplosion",transform.position,transform.eulerAngles);
        gameObject.SetActive(false);
    }
}
