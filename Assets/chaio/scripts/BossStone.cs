using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossStone : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField]private Transform player;
    //[SerializeField]GameObject stoneS01;
    [SerializeField]private Animator animator_small;
    [SerializeField]bool isThrow=false;
    [SerializeField]bool isOnTheGround=false;
    [SerializeField]bool isDead=false;
    [SerializeField]bool isRevival=false;
    [SerializeField]bool isHitSuccess=false;
    [SerializeField]bool isFlyAway=false;
    [SerializeField]private GameObject particle;

    //private Transform flyTo;
    [SerializeField]private Vector3 flyTo;
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
    // Start is called before the first frame update
    void Start()
    {
        isOnTheGround=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isThrow){
            (gameObject.GetComponent(scr)as MonoBehaviour).enabled = false;//禁用dolly track
            velocity.y+=fallDownSpeed*Time.deltaTime;
            
            controller.Move(velocity * Time.deltaTime);
            if(isOnTheGround && !isFlyAway){
                animator_small.SetBool("leaveMain",true);
                if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("run")){
                        transform.LookAt(player);
                        controller.Move(transform.forward * MoveSpeed * Time.deltaTime);//移動朝woomi跑
                        animator_small.SetBool("hitFail",false);  
                }
            }
        }
        //被打會扣寫
           

        
        //打掉死掉
        if(isDead){
            animator_small.SetBool("leaveMain",false);
            animator_small.SetBool("dead",true);
            transform.DOScale(0,1);
            isThrow=false;
            isOnTheGround=false;
            isFlyAway=false;
        }
        else{
            //撞woomi
            if(Vector3.Distance(player.position, transform.position)<hitRange && !isFlyAway){
                animator_small.SetBool("attackRange",true);
                transform.LookAt(player);
                controller.Move(transform.forward * attackSpeed * Time.deltaTime);//攻擊woomi
                //transform.position += transform.forward * attackSpeed * Time.deltaTime;
            }
            if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                hitTimer+=Time.deltaTime;
                if(hitTimer>0.8 && !isHitSuccess){
                    animator_small.SetBool("hitFail",true);  
                    animator_small.SetBool("hitSuccess",false); 
                    animator_small.SetBool("attackRange",false);  
                    hitTimer=0;
                }
            }
            if(isHitSuccess){
                isFlyAway=true;
                isHitSuccess=false;
                animator_small.SetBool("hitFail",false);  
                //flyTo=target.position;
            }
            if(isFlyAway){
                animator_small.SetBool("hitFail",false); 
                animator_small.SetBool("hitSuccess",true);
                if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("FlyInSky")){
                    controller.Move(-transform.forward * flySpeed * Time.deltaTime);//移動遠離woomi
                }
                if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("standUp")){
                    isFlyAway=false;
                    animator_small.SetBool("attackRange",false);
                    animator_small.SetBool("hitSuccess",false); 

                }
            }
        }
        //復活
        if(isRevival){
            isDead=false;
            animator_small.SetBool("dead",false);
            animator_small.SetBool("attackRange",false);
            animator_small.SetBool("hitSuccess",false); 
            animator_small.SetBool("hitFail",false); 
            timer+=Time.deltaTime;
            particle.SetActive(true);
            transform.DOScale(0.4f,revivalTime+1);
            (gameObject.GetComponent(scr)as MonoBehaviour).enabled =true;//打開dolly track
            if(timer > revivalTime){
                isRevival=false;
                particle.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ground")
        isOnTheGround=true;
        if(other.tag=="Player")
        isHitSuccess=true;
    }
    
}
