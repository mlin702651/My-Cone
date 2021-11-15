using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UI;
public class BigStone : MonoBehaviour
{
    [SerializeField]bool canHurt=false;
    [Range(0,100),SerializeField] private int Hp = 100;
    bool isDead=false;
    [SerializeField]private Animator animator_big;
    float timer=0;
    [SerializeField]private Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = Hp.ToString();
        if(Hp<0){
            timer+=Time.deltaTime; 
            animator_big.SetBool("goDead",true);
        }
        if(timer>2.2f){
            gameObject.SetActive(false);
            transform.DOScale(0,0.5f);
            isDead=true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(canHurt){
            if(other.tag =="Player_Magic1"){
            Hp-=15;
            Debug.Log("hurt1!");
            }
            else if(other.tag =="Player_Magic2"){
                Hp-=5;
                Debug.Log("hurt2!");
            }
            else if(other.tag =="Player_Magic3"){
                Hp-=30;
                Debug.Log("hurt3!");
            }
        }
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
