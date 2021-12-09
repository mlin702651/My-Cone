using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterKinoko : MonoBehaviour
{
    public MonsterProfile monsterProfile;
    

    #region Animation
    [Header("Animation")]
    private Animator _kinokoAnimator;
    private int animationIdle;
    private int animationIdleOver;
    private int animationPrepareAttack;
    private int animationRun;
    private int animationRunOver;
    private int animationAttack;
    private int currentAnimationState;

    #endregion

    #region main
    [Header("main")]

    private int kinokoState = 0; //0:idle 1:prepare 2:run 3:attack 4:null
    
    private bool ifStartAttack = false;
    private bool ifTrainingStart = false;
    
    private Rigidbody _kinokoRigidbody;
    private int runCount = 0;
    [SerializeField]private int maxRunCount = 5;
    [SerializeField]private float runSpeed = 2f;
    [SerializeField]private float startCountDown = 0f;

    private CollideInteract collideInteract;
    
    [Header("Hp")]
    private int kinokoHealth = 15;
    private float healthTimer = 0;

    [SerializeField]private Text kinokoHealthText;
    #endregion

    #region facing player 
    [Header("facing player")]

    [SerializeField]private int rotateDamp = 2;
    private Transform lookTarget;
    private Vector3 lookPosition;
    #endregion

     #region hit player 
    // [Header("hit player")]

    // [SerializeField]private bool ifHitPlayer = false;
    // private Rigidbody _playerRigidBody;

    private GameObject Player;
     #endregion
    #region kinoko material
    private Renderer[] kinokoRenderer;
    #endregion

    void ChangeAnimationState(int newAnimationState)
    {
       if(newAnimationState == currentAnimationState) {
            return; //一樣的話就不重新開始播ㄌ
        }
        _kinokoAnimator.Play(newAnimationState);
        
        currentAnimationState = newAnimationState;
    }
    
    private void Awake() {
        _kinokoAnimator = GetComponent<Animator>();
        _kinokoRigidbody = GetComponent<Rigidbody>();
        animationIdle = Animator.StringToHash("Monster_Kinoko_Idle");
        animationIdleOver = Animator.StringToHash("Monster_Kinoko_Idle_Over");
        animationPrepareAttack = Animator.StringToHash("Monster_Kinoko_PrepareAttack");
        animationRun = Animator.StringToHash("Monster_Kinoko_Run");
        animationRunOver = Animator.StringToHash("Monster_Kinoko_Run_Over");
        animationAttack = Animator.StringToHash("Monster_Kinoko_Attack");

        Player = GameObject.Find("NewPlayer");
        //_playerRigidBody = Player.GetComponent<Rigidbody>();
        lookTarget = Player.transform;

    }
    // Start is called before the first frame update
    void Start()
    {
        kinokoState = 0;
        kinokoRenderer = gameObject.GetComponentsInChildren<Renderer>();
        
        kinokoRenderer[0].material.SetFloat("Vector1_2FCC19E7", 1);
        kinokoRenderer[1].material.SetFloat("Vector1_2FCC19E7", 1);
        Debug.Log(kinokoRenderer[0].material.shader.name);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(TrainingThreeManager.instance.canStartTraining){
            if(collideInteract == null){
                collideInteract = GameObject.Find("FightingStartTrigger").GetComponentInChildren<CollideInteract>();
            }
        }
        if(collideInteract != null){
            if(collideInteract.isInteract){
                ifTrainingStart = true;
            }
        }
        
        if(ifTrainingStart){
            startCountDown += Time.deltaTime;
        }
        if(startCountDown>=3){
            ifStartAttack = true;
        }

        healthTimer+= Time.deltaTime;
        kinokoHealthText.text = kinokoHealth.ToString();

        if(healthTimer>=1.0f){
            healthTimer=0;
            if(kinokoHealth==15) return;
            kinokoHealth+=1;
        }

        if(kinokoHealth<=0){
            //die
            //TrainingThreeState.AddMonsterKilledCount();
            TrainingThreeManager.instance.CheckBornNewMonster();
            if(GameManager.instance.onEnemyDeathCallBack != null) GameManager.instance.onEnemyDeathCallBack.Invoke(monsterProfile); //死掉的時候會傳怪物資訊過去
            Destroy(gameObject);
        }
        
        AnimationOverCheck();
        StateCheck();
        
        //HitPlayerCheck();
        
        
        

        //Debug.Log(_kinokoAnimator.GetCurrentAnimatorStateInfo(0).IsName(""));
    }

    private void FacingPlayer(){
        lookPosition = lookTarget.position - transform.position;
        lookPosition.y = 0;
        var rotation = Quaternion.LookRotation(-lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateDamp);
    }

    private void StateCheck(){
        switch(kinokoState){
            case 0:
                ChangeAnimationState(animationIdle);
                
                break;
            case 1:
                //kinokoState = 4;
                ChangeAnimationState(animationPrepareAttack);
               

                FacingPlayer();
                break;
            case 2:
                //kinokoState = 4;
                //_kinokoRigidbody.velocity = transform.forward*Time.deltaTime* -200;
                transform.position += transform.forward*Time.deltaTime* (-runSpeed);
                ChangeAnimationState(animationRun);
                break;
            case 3:
                //_kinokoRigidbody.velocity = transform.forward*Time.deltaTime* -200;
                //kinokoState = 4;
                
                ChangeAnimationState(animationAttack);
                break;
            default:
               break;
        }
    }

    private void AnimationOverCheck(){
        
        if(_kinokoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Kinoko_Idle_Over")){
            ChangeAnimationState(animationIdleOver);
            if(ifStartAttack){
                kinokoState = 1;
                kinokoRenderer[0].material.SetFloat("Vector1_2FCC19E7", 1);
                kinokoRenderer[1].material.SetFloat("Vector1_2FCC19E7", 1);
            }
            else kinokoState = 0;
        }
        else if(_kinokoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Kinoko_PrepareAttack_Over")){
            kinokoState = 2;
        }
        else if(_kinokoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Kinoko_Run_Over")){
            //kinokoState = 2;
            ChangeAnimationState(animationRunOver);
            runCount++;
            if(runCount>=maxRunCount){
                kinokoState = 3;
                kinokoRenderer[0].material.SetFloat("Vector1_2FCC19E7", 0.5f);
                kinokoRenderer[1].material.SetFloat("Vector1_2FCC19E7", 0.5f);
                runCount = 0;
            }
            else kinokoState = 2;
        }
        else if(_kinokoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Kinoko_Attack_Over")){
            kinokoRenderer[0].material.SetFloat("Vector1_2FCC19E7", 0);
            kinokoRenderer[1].material.SetFloat("Vector1_2FCC19E7", 0);
            kinokoState = 0;
        }
    }

    // private void HitPlayerCheck(){
    //     if(ifHitPlayer){
    //         ifHitPlayer = false;
    //         _playerRigidBody.velocity = transform.forward*Time.deltaTime* -400;
    //     }
    // }

    public void StartFighting(){
        ifStartAttack = true;
    }

    private void OnTriggerEnter(Collider other) {
        float kinokoGlow = kinokoRenderer[0].material.GetFloat("Vector1_2FCC19E7");
        if(other.tag=="Player_Magic1"&&kinokoGlow == 0){
            kinokoHealth-=2;
        }
        else if(other.tag=="Player_Magic2"&&kinokoGlow == 0){
            kinokoHealth-=2;
            Debug.Log("hurt2!");
        }
        else if(other.tag=="Player_Magic3"&&kinokoGlow == 0){
            kinokoHealth-=2;
        }
        
    }

    // private void OnCollisionEnter(Collision other) {
    //     if(other.collider.tag=="Player"&&(kinokoState==3)){
    //         Debug.Log("Hit player");
    //         ifHitPlayer=true;
    //     }
    // }
}
