using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField]private Animator animator_small;
     [SerializeField]private GameObject particle;
    [SerializeField] private Transform main;
    public string scr;
    
    public int state=0;
    public float MoveSpeed = 3.0f;

    float size=100;


    // Start is called before the first frame update
    void Start()
    {
        animator_small.SetBool("leaveMain",false);
        animator_small.SetBool("dead",false);
        (gameObject.GetComponent(scr)as MonoBehaviour).enabled = true;
        particle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(state==0){//大石頭旁邊繞
            //(gameObject.GetComponent(scr)as MonoBehaviour).enabled = true;
            animator_small.SetBool("dead",false);
            particle.SetActive(false);
        }
        else if(state==1){//在空中到地板//想做拋物線
            (gameObject.GetComponent(scr)as MonoBehaviour).enabled = false;//禁用dolly track
            
            //落地到state 2
        }
        else if(state==2){//從土裡爬出來
            animator_small.SetBool("leaveMain",true);
            //播完從土裡爬出來的動畫變成 state 3
            if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("run")){
                state=3;
            }
        }
        else if(state==3){//開始跑
            transform.LookAt(player);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            animator_small.SetBool("leaveMain",false);
            //有沒有撞到woomi 
        }
        
        else if(state==4){//死掉之後
            animator_small.SetBool("dead",true);
            //縮放成0
            // main.DOScaleX(0,1);
            // main.DOScaleY(0,1);
            // main.DOScaleZ(0,1);
      
           
        }
        else if(state==5){
            //縮放成1跟打開集氣特效
            (gameObject.GetComponent(scr)as MonoBehaviour).enabled = true;
            particle.SetActive(true);
            // main.DOScaleX(1,5);
            // main.DOScaleY(1,5);
            // main.DOScaleZ(1,5);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.tag=="Ground"){
            state=2;
            Debug.Log(this.name + " " + other.name);
        }
    }
    void SetState(){
        state = 1;
    }

}
