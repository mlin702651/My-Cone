using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StoneController : MonoBehaviour
{
    [SerializeField]BigStone bigStone=null;
    [SerializeField]StoneNMA stone01=null;
    [SerializeField]StoneNMA stone02=null;
    [SerializeField]StoneNMA stone03=null;
    [SerializeField]StoneNMA stone04=null;
    [SerializeField]private GameObject particle;
    [SerializeField]int howManyStone = 4;
    [SerializeField]GameObject deadParticle;
    float timer=0;
    [SerializeField]Vector4 throeTime=new Vector4(3,5,8,11);
    bool bossAttack=false;//大石頭可不可以丟小石頭
    int countThrow=0;

    [SerializeField]GameObject[] teleportPoint= new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(bigStone.getIsDead()){
            gameObject.SetActive(false);
            deadParticle.transform.position=bigStone.getBossPosition();//讓爆炸的位子跟大石頭一樣
            deadParticle.SetActive(true);
        }
        //過幾秒丟一顆石頭
        if(bossAttack){
            stone01.setNotAllStoneDead();
            stone02.setNotAllStoneDead();
            stone03.setNotAllStoneDead();
            stone04.setNotAllStoneDead();
            Debug.Log("in bossAttack");
            timer+=Time.deltaTime;
            if(timer>throeTime.x && countThrow<1){
                stone01.setIsThorw();
                countThrow++;
            }
            else if(timer>throeTime.y && countThrow<2){
                stone02.setIsThorw();
                countThrow++;
            }
            else if(timer>throeTime.z && countThrow<3){
                stone03.setIsThorw();
                countThrow++;
            }
            else if(timer>throeTime.w && countThrow<4){
                stone04.setIsThorw();
                countThrow++;
            }
        }
        //所有小石頭可以被丟出去 粒子打開 大石頭不可以被攻擊
        if(stone01.getIsWaiting()&&stone02.getIsWaiting()&&stone03.getIsWaiting()&&stone04.getIsWaiting()){
            bossAttack=true;
            particle.SetActive(true);
            bigStone.setCanNotHurt();
        }
        //所有小石頭丟出去之後 timer歸0     
        if(countThrow >= howManyStone ){
            countThrow=0;
            bossAttack=false;
            timer=0;
        }
        //所有石頭死掉之後把粒子關掉
        if(stone01.getIsDead()&&stone02.getIsDead()&&stone03.getIsDead()&&stone04.getIsDead()){
            particle.SetActive(false);
            stone01.setIsAllStoneDead();
            stone02.setIsAllStoneDead();
            stone03.setIsAllStoneDead();
            stone04.setIsAllStoneDead();
            bigStone.setCanHurt();//大石頭可以被攻擊
            //countThrow=0;
        }

        if(bigStone.GetTeleport()){
            DoTeleport();
        }
    }

     void DoTeleport(){
        int TeleportCount;
        TeleportCount=Random.Range(0,3);
        transform.position=teleportPoint[TeleportCount].transform.position;
    }
}
