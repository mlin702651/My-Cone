using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlower : MonoBehaviour
{
    [SerializeField]private Boss boss;
    private void OnTriggerEnter(Collider other) {
        if(other.tag =="Player_Magic1"){
            boss.HurtRealHealth(15);
        }
        if(other.tag =="Player_Magic2"){
            boss.HurtRealHealth(5);
        }
        if(other.tag =="Player_Magic3"){
            boss.HurtRealHealth(10);
        }
    }
}
