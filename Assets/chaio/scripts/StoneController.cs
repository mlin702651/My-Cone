using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    [SerializeField]BossStone stone01;
    [SerializeField]BossStone stone02;
    [SerializeField]private GameObject particle;
    [SerializeField]int howManyStone = 2;
    float timer=0;

    bool bossAttack=false;
    int count=0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossAttack){//過幾秒丟一顆石頭
            timer+=Time.deltaTime;
            if(timer>5 && count<1){
                stone01.setIsThorw();
                count++;
            }
            if(timer>8 && count<2){
                stone02.setIsThorw();
                count++;
            }
        }
        if(stone01.getIsWaiting()&&stone02.getIsWaiting()){//所有石頭可以攻擊 粒子打開
            bossAttack=true;
            particle.SetActive(true);
        }
        if(count >= howManyStone ){//所有石頭丟出去之後 timer歸0
            bossAttack=false;
            timer=0;
        }
        if(stone01.getIsDead()&&stone02.getIsDead()){//所有石頭死掉之後把粒子關掉
            particle.SetActive(false);
            stone01.setIsRevival();
            stone02.setIsRevival();
            count=0;
        }
    }
}
