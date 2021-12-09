using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UI;

public class BigStone : MonoBehaviour
{
    [SerializeField]bool canHurt=false;
    [SerializeField]private Transform Player;
    [Range(0,100),SerializeField] private int Hp = 100;
    bool isDead=false;
    [SerializeField]private Animator animator_big;
    float timer=0;
    [SerializeField]private Text healthText;
    float countPosition=0;
    [SerializeField]Vector3 offset=new Vector3(0,0,0);
    [SerializeField]float radius=5.0f;
    [SerializeField]float speed=0.5f;
    // Start is called before the first frame update
    [SerializeField]GameObject[] teleportPoint= new GameObject[3];
    int damage=0;
    bool teleport=false;
    float teleportTimer=0;
    //CinemachineDollyCart cart;
    void Start()
    {
        
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
        if(timer>2.1f){
            gameObject.SetActive(false);
            transform.DOScale(0,0.5f);
            isDead=true;
        }

        if(Hp>5){
            if(damage>=35){
                teleport=true;
                damage=0;
                transform.DOScale(0,0.5f);
                FunctionTimer.Create(()=>Enlarge(),0.3f);//好讚的東西
            }
            else{
                teleport=false;
            }
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

}
