using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UI;

public class BigStone : MonoBehaviour
{
    NavMeshAgent navMeshAgent=null;
    [SerializeField]bool canHurt=false;
    [SerializeField]private Transform Player;
    [Range(0,100),SerializeField] private int Hp = 100;
    [SerializeField] Slider slider;
    bool isDead=false;
    [SerializeField]private Animator animator_big;
    float timer=0;
    [SerializeField]private Text healthText;
    float countPosition=0;
    [SerializeField]Vector3 offset=new Vector3(0,0,0);
    [SerializeField]float radius=5.0f;
    [SerializeField]float speed=0.5f;

    [SerializeField]GameObject currentParticle=null;

    // Start is called before the first frame update
    int damage=0;
    bool teleport=false;

    //震波動畫
    private Vector3 OriginPosition = Vector3.zero;
    [SerializeField] private Vector3 A1AnimationTarget = new Vector3(0,5f,0);
    [SerializeField] private float A1AnimationStartDur = 3f;
    [SerializeField] private Ease A1AnimationStartE = Ease.Linear;
    [SerializeField] private float A1AnimationEndDur = 1f;
    [SerializeField] private Ease A1AnimationEndE = Ease.Linear;
    [SerializeField] private float A1AnimationDelay = 3f;
    void Start()
    {
        navMeshAgent=GetComponent<NavMeshAgent>(); 
        // OriginPosition = transform.position;
        // print(OriginPosition);
    }

    // Update is called once per frame
    void Update()
    {
        countPosition+=speed*Time.deltaTime; 
        healthText.text = Hp.ToString();
        if(Hp<=0){
            timer+=Time.deltaTime; 
            animator_big.SetBool("goDead",true);
        }
        if(timer>1.9f){
            //gameObject.SetActive(false);
            transform.DOScale(0.01f,0.5f);
            isDead=true;
        }

        if(Hp>5){
            if(damage>=35){
                teleport=true;
                damage=0;
                transform.DOScale(0.01f,0.3f);
                //Instantiate(teleportParticle,transform);
                //teleportParticle.transform.parent=teleportParticleParent.transform;
                FunctionTimer.Create(()=>Enlarge(),0.5f);//好讚的東西
            }
            else{
                teleport=false;
            }
        }
        slider.value = Hp;
        navMeshAgent.SetDestination(Player.transform.position);

        if(canHurt){
            currentParticle.SetActive(true);
            // DOTween.Sequence()
            //                 .Append(transform.DOMoveY(A1AnimationTarget.y, A1AnimationStartDur).SetEase(A1AnimationStartE))
            //                 .Append(transform.DOMoveY(OriginPosition.y, A1AnimationEndDur).SetEase(A1AnimationEndE));
        }
        else{
            currentParticle.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(Hp>0){
            if(canHurt){
                if(other.tag =="Player_Magic1"){
                    damage+=15;
                    Hp-=15;
                    Debug.Log("hurt1!");
                }
                else if(other.tag =="Player_Magic2"){
                    damage+=5;
                    Hp-=5;
                    Debug.Log("hurt2!");
                }
                else if(other.tag =="Player_Magic3"){
                    damage+=20;
                    Hp-=20;
                    Debug.Log("hurt3!");
                }
            }
        }
    }
    public bool GetTeleport(){
        return(teleport);
    }
    public void Enlarge(){
        transform.DOScale(0.4f,0.5f);
    }
    public void setCanHurt(){
        canHurt=true;
    }
    public void setCanNotHurt(){
        canHurt=false;
    }
    public bool getIsDead(){
        return(isDead);
    }
    public Vector3 getBossPosition(){
        return(transform.position);
    }
    public Transform getPosition(){
        return(transform);
    }

    
}
