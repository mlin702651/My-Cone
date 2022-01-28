using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class BossStone : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField]private Transform player;
    [SerializeField]private Animator animator_small;
    [SerializeField]bool isThrow=false;
    [SerializeField]bool isOnTheGround=false;
    [SerializeField]bool isDead=false;
    [SerializeField]bool isRevival=false;
    [SerializeField]bool isHitSuccess=false;
    [SerializeField]bool isHitFail=false;
    [SerializeField]bool isFlyAway=false;
    [SerializeField]bool isWaiting=true;
    [SerializeField]private GameObject particle;
    [SerializeField]private Transform failTarget;
    [SerializeField]private GameObject particleHit;
    public float MoveSpeed = 3.0f;
    public float attackSpeed = 6.0f;
    public float flySpeed = 4.5f;
    public float fallDownSpeed=-9.8f;
    public float revivalTime=3;
    public string scr;
    public float hitRange=3;
    Vector3 velocity=new Vector3(0,0,0);
    float timer=0;
    float hitTimer=0;
    float stayTimer=0;
   
    
   
    // Start is called before the first frame update
    void Start()
    {
        isOnTheGround=false;
        particleHit.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if(isThrow){
            isWaiting=false;
            (gameObject.GetComponent(scr)as MonoBehaviour).enabled = false;//禁用dolly track
            velocity.y+=fallDownSpeed*Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            if(isOnTheGround && !isFlyAway){
                animator_small.SetBool("leaveMain",true);
                if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("run")){
                    transform.LookAt(player);
                    controller.Move(transform.forward * MoveSpeed * Time.deltaTime);//移動朝woomi跑
                    animator_small.SetBool("hitFail",false); 
                    hitTimer=0;
                }
            }
        }
        //被打會扣寫
           

        
        //打掉死掉
        if(isDead){
            animator_small.SetBool("leaveMain",false);
            animator_small.SetBool("dead",true);
            transform.DOScale(0.1f,1);
            isThrow=false;
            isOnTheGround=false;
            isFlyAway=false;
        }
        else{
            //撞woomi
            if(Vector3.Distance(player.position, transform.position)<hitRange && !isFlyAway){
                animator_small.SetBool("attackRange",true);
                //transform.LookAt(player);
            }
            if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                hitTimer+=Time.deltaTime;
                //animator_small.SetBool("hitFail",false);  
                Debug.Log(hitTimer);
                isHitFail=false;
                //速度
                controller.Move(transform.forward * MoveSpeed * 2 * Time.deltaTime);//移動朝woomi跑
                if(hitTimer>0.45 && !isHitSuccess){//只會跑一次
                    animator_small.SetBool("hitFail",true);  
                    animator_small.SetBool("hitSuccess",false); 
                    animator_small.SetBool("attackRange",false);  
                    hitTimer=0;
                    stayTimer=0; 
                    isHitFail=true;
                }
            }
            if(isHitSuccess){
                particleHit.SetActive(true);
                isFlyAway=true;
                isHitSuccess=false;
                hitTimer=0;
              
            }
            if(isFlyAway){
                animator_small.SetBool("hitFail",false); 
                animator_small.SetBool("hitSuccess",true);
                if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("FlyInSky")){
                    controller.Move(-transform.forward * flySpeed * Time.deltaTime);//移動遠離woomi
                    particleHit.SetActive(false);
                }
                if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("standUp")){
                    isFlyAway=false;
                    animator_small.SetBool("attackRange",false);
                    animator_small.SetBool("hitSuccess",false); 
                }
            }
        }
        //復活
        if(isRevival){//重設參數
            isDead=false;
            animator_small.SetBool("dead",false);
            animator_small.SetBool("attackRange",false);
            animator_small.SetBool("hitSuccess",false); 
            animator_small.SetBool("hitFail",false); 
            (gameObject.GetComponent(scr)as MonoBehaviour).enabled =true;//打開dolly track
            timer+=Time.deltaTime;
            particle.SetActive(true);
            transform.DOScale(0.4f,revivalTime+1);
            if(timer > revivalTime){
                isRevival=false;
                particle.SetActive(false);
                isWaiting=true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ground")
        isOnTheGround=true;
        if(other.tag=="Player"){
            isHitSuccess=true;
        }
        
    }

    public void setIsThorw(){
        isThrow=true;
    }
    public bool getIsWaiting(){
        return(isWaiting);
    }
     public void setIsRevival(){
        isRevival=true;
    }
    public bool getIsRevival(){
        return(isRevival);
    }
     public bool getIsDead(){
        return(isDead);
    }
    
}
