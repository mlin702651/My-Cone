using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterDidi : MonoBehaviour
{
    
    public MonsterProfile monsterProfile;
    private NavMeshAgent _agent;

    private int didiState = 0; //0:underground 1:idle 2:flee

    [SerializeField]private float underGroundOffset = -0.14f;
    [SerializeField]private float onGroundOffset = 0.1f;
    private float offsetInterpolation = 0;

    #region Animation
    [SerializeField]private bool isAttacked = false;
    [SerializeField]private bool ifStartPopOut = false;
    [SerializeField]private bool ifStartFall = false;
    [SerializeField]private bool ifGoal = false;
    private Animator _didiAnimator;
    private int currentAnimationState;

    private int animationIdle;
    private int animationPopOut;
    private int animationRun;

    [SerializeField]private float didiPopOutDelay;

    #endregion

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Goal;

    [SerializeField] private float DidiDistanceRun = 4.0f;
    [SerializeField] private float DidiDistanceGoal = 8.0f;
    
    
    private void Awake() {
        _didiAnimator = GetComponent<Animator>();
        animationIdle = Animator.StringToHash("MonsterDiDi_Idle");
        animationPopOut = Animator.StringToHash("MonsterDiDi_PopOut");
        animationRun = Animator.StringToHash("MonsterDiDi_Run");
    }
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.baseOffset = underGroundOffset;
        ChangeAnimationState(animationIdle);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        float distanceToGoal = Vector3.Distance(transform.position, Goal.transform.position);

        //Debug.Log("Distance" + distanceToPlayer);

        if(_agent.baseOffset>=5){
            if(GameManager.instance.onEnemyDeathCallBack != null) GameManager.instance.onEnemyDeathCallBack.Invoke(monsterProfile); //死掉的時候會傳怪物資訊過去
            Destroy(gameObject);
        }
        if(ifGoal){
             _agent.baseOffset += Time.deltaTime*0.5f;
            return;
        }
        
        if(isAttacked){
            isAttacked = false;
            PopOut();
        }
        if(ifStartPopOut){
            _agent.baseOffset = Mathf.Lerp(underGroundOffset,onGroundOffset,offsetInterpolation);
            offsetInterpolation += Time.deltaTime/0.3f;
            if(offsetInterpolation>=1) {
                ifStartPopOut = false;
                ifStartFall = true;
            }
        }
        if(ifStartFall){
            _agent.baseOffset = Mathf.Lerp(0,onGroundOffset,offsetInterpolation);
            offsetInterpolation -= Time.deltaTime/0.3f;
            if(offsetInterpolation<=0) ifStartFall = false;
        }
        if(currentAnimationState==animationPopOut) return;
        if(distanceToGoal < DidiDistanceGoal){
            ChangeAnimationState(animationRun);
            Vector3 dirToGoal = transform.position - Goal.transform.position;
            Vector3 newPos = transform.position - dirToGoal;
            _agent.SetDestination(newPos);
        } 
        else if(distanceToPlayer < DidiDistanceRun){
            if(didiState==0) return;
            ChangeAnimationState(animationRun);
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            _agent.SetDestination(newPos);
        }
        else ChangeAnimationState(animationIdle);

    }

    void ChangeAnimationState(int newAnimationState)
    {
       if(newAnimationState == currentAnimationState) {
            return; //一樣的話就不重新開始播ㄌ
        }
        _didiAnimator.Play(newAnimationState);
        
        currentAnimationState = newAnimationState;
    }

    void PopOut(){
        if(offsetInterpolation>=1) return;
        ChangeAnimationState(animationPopOut);
        //didiPopOutDelay = _didiAnimator.GetCurrentAnimatorStateInfo(0).length;
        //didiPopOutDelay = _didiAnimator.Get
        ifStartPopOut = true;
        Invoke("PlayDidiIdleAnimation",didiPopOutDelay);


    }

    void PlayDidiIdleAnimation(){
        ChangeAnimationState(animationIdle);
    }
    private void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player_Magic3"){
            Debug.Log("Bomb Hit didi!!");
            didiState = 1;
            isAttacked = true;
        }
        if(other.tag =="Training03_Goal"){
            ifGoal = true;
        }
    }

    
}
