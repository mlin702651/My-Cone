using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private MonsterProfile monsterProfile;
    [SerializeField]private GameObject islandTeleport;
    [SerializeField]private TempObjectPooler attackPooler;
    [Range(1,3),SerializeField] private int _iAttackType = 1;
    [SerializeField] private bool _bIsAngry = false;
    private float protectHealth = 100;
    [SerializeField]private Image protectHealthUI;
    private float health = 100;
    private bool isDead;
    [SerializeField]private Image healthUI;

    private bool _bAngryTimer = false;
    private float _fAngryTimer = 0;
    private float _fAttackAnimationTR = 0f;
    private int _iAttackAnimationStatus = 2;//1=動畫不可以播 2=動畫可以播 3=動畫播完
    private float _fAttackTimer = 0;
    [SerializeField] private float _fAttackFrequency = 5;//幾秒攻擊一次
    private float _fAttackFrequencyTr = 0f;//攻擊頻率計時器
    private float _fAttack3RandomTime = 1f;
    [SerializeField]Transform attackBornPosition;

    //Attack 1
    [Header("Attack 1")]
    [SerializeField] private int _iA1Quan = 5;
    private int _iA1NowQuan = 0;
    [SerializeField] private Vector3 A1AnimationTarget = new Vector3(0,5f,0);
    [SerializeField] private float A1AnimationStartDur = 3f;
    [SerializeField] private Ease A1AnimationStartE = Ease.Linear;
    [SerializeField] private float A1AnimationEndDur = 1f;
    [SerializeField] private Ease A1AnimationEndE = Ease.Linear;
    [SerializeField] private float A1AnimationDelay = 3f;

    //Attack 2
    [Header("Attack 2")]
    [SerializeField] private Vector3 A2AnimationTarget = new Vector3(0,5f,0);
    [SerializeField] private float A2AnimationStartDur = 3f;
    [SerializeField] private Ease A2AnimationStartE = Ease.Linear;
    [SerializeField] private float A2AnimationEndDur = 1f;
    [SerializeField] private Ease A2AnimationEndE = Ease.Linear;
    [SerializeField] private float A2AnimationDelay = 3f;
    
    private Vector3 OriginPosition = Vector3.zero;

    //Lift Flower
    [Header("Attack 3")]
    [SerializeField]private GameObject FlowerSet;
    [SerializeField]private float FlowerLiftDuration = 3f;
    [SerializeField]private float FlowerDropDuration = 4f;
    [SerializeField] private Ease FlowerLiftEase = Ease.Linear;
    [SerializeField] private Ease FlowerDropEase = Ease.Linear;

    private Vector3 FlowerOriginalTransform;
    [SerializeField]private Vector3 FlowerTargetTransform;
    [SerializeField]private GameObject leftBud;
    [SerializeField]private GameObject rightBud;
    private bool isFlowerSetAppear = false;
    private Vector3 LeftBudsOriginalTransform = new Vector3(0,0,0);
    private Vector3 RightBudsOriginalTransform = new Vector3(90,0,0);
    [SerializeField]private Vector3 LeftBudsOpenTransform;
    [SerializeField]private Vector3 RightBudsOpenTransform;
    [SerializeField]private float BudsOpenDuration = 2f;
    [SerializeField]private float BudsCloseDuration = 1f;
    [SerializeField] private Ease BudsOpenEase = Ease.Linear;
    [SerializeField] private Ease BudsCloseEase = Ease.Linear;

    //look at player
    private GameObject targetPlayer;
    private Transform playerTransform;

    //animation
    private Animator _garbageAnimator;
    private int currentAnimationState;

    private int animationIdle;
    private int animationBulletAttack;
    private int animationWallAttack;
    private int animationAngry;



    //private bool isChanged;
    private void Awake() {
        _garbageAnimator = GetComponent<Animator>();
        
        animationIdle = Animator.StringToHash("Garbage_Idle");
        animationBulletAttack = Animator.StringToHash("Garbage_Bullet");
        animationWallAttack = Animator.StringToHash("Garbage_Wall");
        animationAngry = Animator.StringToHash("Garbage_Angry");
    }

    void ChangeAnimationState(int newAnimationState)
    {
       if(newAnimationState == currentAnimationState) {
            return; //一樣的話就不重新開始播ㄌ
        }
        _garbageAnimator.Play(newAnimationState);
        
        currentAnimationState = newAnimationState;
    }
    
    void Start()
    {
        //attackPooler = ObjectPooler.Instance;
        OriginPosition = transform.position;
        print(OriginPosition);
        FlowerOriginalTransform = FlowerSet.transform.position;
        //LeftBudsOriginalTransform = leftBud.transform.eulerAngles;
        //RightBudsOriginalTransform = rightBud.transform.eulerAngles;
        //Debug.Log(transform.eulerAngles);

        targetPlayer = FindObjectOfType<PlayerStateMachine>().gameObject;

        ChangeAnimationState(animationIdle);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDead) return;
        
        targetPlayer = FindObjectOfType<PlayerStateMachine>().gameObject;
        transform.LookAt(new Vector3(targetPlayer.transform.position.x,transform.position.y,targetPlayer.transform.position.z));
        
        // _iAttackType = 1;
        // print(transform.rotation);
        // attackPooler.SpawnFromPool("Cube",transform.position-new Vector3(0,2,0),Quaternion.identity);
        if(!_bIsAngry){   //如果boss沒有生氣
            if(isFlowerSetAppear){
                FlowerSet.transform.DOMoveY(FlowerOriginalTransform.y,FlowerLiftDuration).SetEase(FlowerLiftEase);
                isFlowerSetAppear = false;
            }
            
            //leftBud.transform.DOLocalRotate(LeftBudsOriginalTransform,BudsCloseDuration).SetEase(BudsCloseEase);

            //rightBud.transform.DOLocalRotate(RightBudsOriginalTransform,BudsCloseDuration).SetEase(BudsCloseEase);
            if(_iAttackAnimationStatus == 1){
               _iAttackAnimationStatus = 2;
            }
            _fAttackTimer += Time.deltaTime;
            _fAttackFrequencyTr += Time.deltaTime;
            
            
            if(_fAttackFrequencyTr>=_fAttackFrequency){
                _fAttackAnimationTR+=Time.deltaTime;
                if(_iAttackType==1){
                    //print("type 1");
                    BulletAttack();
                        
                }
                else if(_iAttackType==2){
                    //print("type 2");
                    WallAttack();
                }
            }
            
        }
        else{
            AngryAttack();
        }
        
        if(_bAngryTimer){
            _fAngryTimer+= Time.deltaTime;
            if(_fAngryTimer>=15){
                _fAngryTimer = 0;
                _bAngryTimer = false;
                _bIsAngry = false;
                protectHealth = 100;
                protectHealthUI.fillAmount = protectHealth/100;
                ChangeAnimationState(animationIdle);

            }
        }
        
    }

    private void BulletAttack(){
        if( _iAttackAnimationStatus == 2){
            ChangeAnimationState(animationBulletAttack);
            DOTween.Sequence()
                .Append(transform.DOMoveY(A1AnimationTarget.y, A1AnimationStartDur).SetEase(A1AnimationStartE))
                .Append(transform.DOMoveY(OriginPosition.y, A1AnimationEndDur).SetEase(A1AnimationEndE));
            _iAttackAnimationStatus = 3;
        }
        if(_fAttackAnimationTR>=A1AnimationDelay&&_iA1NowQuan<=_iA1Quan){
            if(_fAttackTimer>=0.3){
                _fAttackTimer = 0;
                attackPooler.SpawnFromPool("bullet",attackBornPosition.position,transform.eulerAngles+new Vector3(0,67.5f,0));
                attackPooler.SpawnFromPool("bullet",attackBornPosition.position,transform.eulerAngles+new Vector3(0,112.5f,0));
                attackPooler.SpawnFromPool("bullet",attackBornPosition.position,transform.eulerAngles+new Vector3(0,155.5f,0));
                attackPooler.SpawnFromPool("bullet",attackBornPosition.position,transform.eulerAngles+new Vector3(0,200.5f,0));
                //print(transform.rotation);
                //print(Quaternion.identity);
                _iA1NowQuan++;
                
            }
            if(_iA1NowQuan==_iA1Quan){
                _fAttackAnimationTR = 0;
                _iA1NowQuan = 0;
                _fAttackFrequencyTr = 0;
                _iAttackAnimationStatus = 1;
                ChangeAnimationState(animationIdle);
                _iAttackType = Random.Range(1,3);//攻擊隨機1/2
            }
        }
    }

    private void WallAttack(){
        if( _iAttackAnimationStatus == 2){
            ChangeAnimationState(animationWallAttack);
            DOTween.Sequence()
                .Append(transform.DOMoveY(A2AnimationTarget.y, A2AnimationStartDur).SetEase(A2AnimationStartE))
                .Append(transform.DOMoveY(OriginPosition.y, A2AnimationEndDur).SetEase(A2AnimationEndE));
            _iAttackAnimationStatus = 3;
        }
    
        if(_fAttackAnimationTR>=A2AnimationDelay){
            attackPooler.SpawnFromPool("wall",attackBornPosition.position,transform.eulerAngles);
            attackPooler.SpawnFromPool("wallParticle",attackBornPosition.position,new Vector3(-90,0,0));
            attackPooler.SpawnFromPool("wallSmoke",attackBornPosition.position,new Vector3(-90,0,0));
            _fAttackAnimationTR = 0;
            _fAttackFrequencyTr = 0;
            //_bIsAngry = true;
            _iAttackAnimationStatus = 1;
            ChangeAnimationState(animationIdle);
            _iAttackType = Random.Range(1,3);//攻擊隨機1/2
        }
    }

    private void AngryAttack(){
            ChangeAnimationState(animationAngry);
            if(!isFlowerSetAppear){
                FlowerSet.transform.DOMoveY(FlowerTargetTransform.y,FlowerDropDuration).SetEase(FlowerDropEase);
                isFlowerSetAppear = true;
            }
            //leftBud.transform.DOLocalRotate(LeftBudsOpenTransform,BudsOpenDuration).SetEase(BudsOpenEase);
        //    Debug.Log(rightBud.transform.rotation.x);

            //rightBud.transform.DOLocalRotate(RightBudsOpenTransform,BudsOpenDuration).SetEase(BudsOpenEase);
            if(rightBud.transform.rotation.x<=-0.64)rightBud.transform.Rotate(-RightBudsOpenTransform.x,0,0,Space.Self);
            if(rightBud.transform.rotation.x<=-0.64)rightBud.transform.Rotate(-RightBudsOpenTransform.x,0,0,Space.Self);

            //print("type 3");
            _fAttackTimer += Time.deltaTime;
            if(_fAttackTimer>=_fAttack3RandomTime){
                _fAttackTimer = 0;
                _fAttack3RandomTime = Random.Range(0.5f,1.5f);
                attackPooler.SpawnFromPool("bomb",transform.position,transform.eulerAngles);
                //attackPooler.SpawnFromPool("RainExplode",transform.position,transform.eulerAngles);

        }  
    }

    private void HurtProtectHealth(float damage){
        if(_bIsAngry) return;
        protectHealth -= damage;
        print(protectHealth);
        protectHealthUI.fillAmount = protectHealth/100f;
        if(protectHealth<=0){
            protectHealth = 100;
            _bIsAngry = true;
            _bAngryTimer = true;
        }
    }

    public void HurtRealHealth(float damage){
        if(!_bIsAngry) return;
        health -= (damage/2);
        healthUI.fillAmount = health/100f;
        if(health <= 0){
            print("boss is dead");
            transform.DOMoveY(-20,10f).OnComplete(Dead);;
            //Destroy(gameObject);
        }
    }

    private void Dead(){
        isDead = true;
        islandTeleport.SetActive(true);
        if(GameManager.instance.onEnemyDeathCallBack != null) GameManager.instance.onEnemyDeathCallBack.Invoke(monsterProfile); //死掉的時候會傳怪物資訊過去
        Destroy(gameObject);
    }

    

    private void OnTriggerEnter(Collider other) {
        if(other.tag =="Player_Magic1"){
            HurtProtectHealth(15);
            //HurtRealHealth(15);
        }
        if(other.tag =="Player_Magic2"){
            HurtProtectHealth(5);
            //HurtRealHealth(5);
        }
        if(other.tag =="Player_Magic3"){
            HurtProtectHealth(20);
            //HurtRealHealth(10);
        }
       

    }
}
