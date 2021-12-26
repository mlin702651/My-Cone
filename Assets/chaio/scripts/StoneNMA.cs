using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UI;
public class StoneNMA : MonoBehaviour
{
    NavMeshAgent navMeshAgent=null;
    [SerializeField]private Animator animator_small;
    [SerializeField] private StoneMode stoneMode=StoneMode.aroundMain; 
    [Range(0,40),SerializeField] private int Hp = 30;
    [SerializeField] Slider slider;
    [SerializeField]private Text healthText;
    [SerializeField]Transform straightTarget;
    [SerializeField]Transform player;
    [SerializeField]private GameObject particleHit;
    [SerializeField]private GameObject particleRev;
    [SerializeField] string scr="CinemachineDollyCart";//dolly track的腳本名子
    [Header("Change State")]
    [SerializeField]bool isOnTheGround=false;
    [SerializeField]bool isDead=false;
    [SerializeField]bool isAllStoneDead=false;
    [SerializeField]bool isWaiting=true;
    [Header("Other")]
    [SerializeField]bool isThrow=false;//大石頭控制
    [SerializeField]bool isHit=false;
    [SerializeField]bool isRevival=false;
    [SerializeField]bool isCoolDown=false;
    [SerializeField]bool isTimerOn=false;
    [SerializeField] bool isTurnning=false;
    //範圍
    [Header("Value")]
    [SerializeField]float hitRange=5;
    [SerializeField]float stopRange=1.2f;
    [SerializeField]float moveSpeed=2.5f;
    [SerializeField]float attackSpeed=3.5f;
    [SerializeField]float coolTime=1;
    [SerializeField]float revivalSize=0.2f;
    [SerializeField]float runningSize=0.4f;
    public float revivalTime=3;
    float toPlayerDistance;
    float timer=0;
    float coolTimer=0;
    float revTimer=0;
    // //旋轉球球
    // [SerializeField] private float BornTime = 0.2f;
    // [SerializeField] private float _bulletSpeed = 10.0f;
    [SerializeField] GameObject bullet;
    GameObject bulletClone;
    bool canShoot=true;
    float shootTimer=0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent=GetComponent<NavMeshAgent>();
        bulletClone=Instantiate(bullet);
        bulletClone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Hp<0){
            healthText.text ="0";
        }
        else{
            healthText.text = Hp.ToString();
        }
        slider.value = Hp;
        toPlayerDistance =Vector3.Distance(player.position, transform.position);
        switch(stoneMode){
            case StoneMode.aroundMain:
                //做事
                DoAroundMain();
                //切換狀態
                if(isOnTheGround){
                    stoneMode=StoneMode.onTheGroung;
                }
                break;
            case StoneMode.onTheGroung:
                //做事
                DoOnTheGround();
                //切換狀態
                if(isDead){
                    stoneMode=StoneMode.dead;
                }
                slider.gameObject.SetActive(true);
                break;
            case StoneMode.dead:
                //做事
                DoDead();
                //切換狀態
                if(isAllStoneDead){
                    stoneMode=StoneMode.revival;
                }
                break;
            case StoneMode.revival:
                //做事
                DoRevival();
                //切換狀態
                if(isWaiting){
                    stoneMode=StoneMode.aroundMain;
                }
                break;
            default:
                break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Groung"){
            isOnTheGround=true;
        }
        if(other.tag=="Player"){
            isHit=true;
            particleHit.SetActive(true);
        }
        if(other.tag =="Player_Magic1"){
            Hp-=15;
            Debug.Log("hurt1!");
        }
        else if(other.tag =="Player_Magic2"){
            Hp-=5;
            Debug.Log("hurt2!");
        }
        else if(other.tag =="Player_Magic3"){
            Physics.IgnoreCollision(other,gameObject.GetComponent<SphereCollider>());
            Hp-=20;
            Debug.Log("hurt3!");
        }
    }
    private void DoAroundMain(){
        if(isThrow){
            (gameObject.GetComponent(scr)as MonoBehaviour).enabled = false;//禁用dolly track
            if(navMeshAgent.isOnNavMesh){
                Debug.Log("isOnNavMesh");
                isOnTheGround=true;
                isWaiting=false;
            }
        }
    }
    private void DoOnTheGround(){

        // if(shootTimer>10.0f){
        //     shoot();
        //     shootTimer=0;
        //     //FunctionTimer.Create(()=>shoot(),2.0f);//發射球球
        // }
        // else{
        //     shootTimer+=Time.deltaTime;
        // }


        bulletClone.SetActive(true);
        bulletClone.transform.position=transform.position;
        //
        animator_small.SetBool("leaveMain",true);
        transform.DOScale(runningSize,0.5f);
        if(animator_small.GetCurrentAnimatorStateInfo(0).IsName("standUp")){//撥這個動畫的時候轉向
            navMeshAgent.SetDestination(player.transform.position);
            Debug.Log("turnning");
        }
        if(isCoolDown){//冷卻時間 跌倒的   
            coolTimer+=Time.deltaTime;
            if(coolTimer>coolTime){//重設參數們
                isCoolDown=false;
                coolTimer=0;
                animator_small.SetBool("standToRun",true);
                Debug.Log("standToRun,true");
                particleHit.SetActive(false);//關掉撞到player的粒子
                animator_small.SetBool("hitSuccess",false);
                timer=0;
                isTurnning=true;
            }
            else{
                Debug.Log("isCoolDown speed=0");
                navMeshAgent.SetDestination(player.transform.position);
                GetComponent<NavMeshAgent>().speed=0;
            }
        }
        else{//不在冷卻時間
            animator_small.SetBool("standToRun",false);
            if(isTimerOn){//攻擊一次的循環時間
                timer+=Time.deltaTime;
                navMeshAgent.isStopped=false;
                navMeshAgent.SetDestination(straightTarget.transform.position);
                GetComponent<NavMeshAgent>().speed=attackSpeed;
                animator_small.SetBool("attackRange",true);
                if(isHit){
                    animator_small.SetBool("hitSuccess",true);
                    isCoolDown=true;
                    isTimerOn=false;
                    isHit=false;
                    timer=0;
                    Debug.Log("success");
                }
                else if(timer>0.5f){//超過0.5秒沒撞到東西視為失敗
                    isCoolDown=true;
                    //animator_small.SetBool("hitFail",true);
                    animator_small.SetBool("hitSuccess",true);//先暫時這樣 只要攻擊都會進冷卻 
                    timer=0;
                    isTimerOn=false;
                    Debug.Log("fail");
                }
            }
            else{
                if(toPlayerDistance<hitRange){//攻擊的範圍
                    Debug.Log("toPlayerDistance<hitRange");
                    navMeshAgent.SetDestination(player.transform.position);
                    isTimerOn=true;
                    if(toPlayerDistance<stopRange){
                        navMeshAgent.isStopped=true;
                    }
                }
                else{//平常在走路
                    Debug.Log("toPlayerDistance>hitRange");
                    navMeshAgent.isStopped=false;
                    isHit=false;
                    animator_small.SetBool("hitSuccess",false);
                    animator_small.SetBool("hitFail",false);
                    animator_small.SetBool("attackRange",false);
                    navMeshAgent.SetDestination(player.transform.position);
                    GetComponent<NavMeshAgent>().speed=moveSpeed;
                    
                }
            }
            if(Hp<=0){
                isDead=true;
            }
        }      
    }
    private void DoDead(){
        //
        bulletClone.SetActive(false);
        //  

        animator_small.SetBool("leaveMain",false);
        animator_small.SetBool("leaveMain",false);
        animator_small.SetBool("dead",true);
        transform.DOScale(0,1);
        isThrow=false;
        isOnTheGround=false;
    }
    private void DoRevival(){
        isDead=false;
        animator_small.SetBool("dead",false);
        animator_small.SetBool("attackRange",false);
        animator_small.SetBool("hitSuccess",false); 
        animator_small.SetBool("hitFail",false); 
        (gameObject.GetComponent(scr)as MonoBehaviour).enabled =true;//打開dolly track
        particleRev.SetActive(true);
        revTimer+=Time.deltaTime;
        transform.DOScale(revivalSize,revivalTime+1);
        if(revTimer > revivalTime){
            isRevival=false;
            particleRev.SetActive(false);
            isWaiting=true;
            revTimer=0;
            Debug.Log("end revival");       
        }
        Hp=30;
    }
    public enum StoneMode{
        aroundMain,
        onTheGroung,
        dead,
        revival
    }
    
    public void setIsThorw(){
        isThrow=true;
    }
    public void setIsAllStoneDead(){
        isAllStoneDead=true;
    } 
    public void setNotAllStoneDead(){
        isAllStoneDead=false;
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
    public bool getIsWaiting(){
        return(isWaiting);
    }

    void shoot(){
        Instantiate(bullet,transform.position,transform.rotation);
        Destroy(bullet,11.0f);
    } 
}
