using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StoneNMA : MonoBehaviour
{
  
    NavMeshAgent navMeshAgent=null;
    [SerializeField] private StoneMode stoneMode=StoneMode.aroundMain; 
    [SerializeField]Transform player;

    [SerializeField]bool isThrow=false;
    [SerializeField]bool isOnTheGround=false;
    [SerializeField]bool isDead=false;
    [SerializeField]bool isAllStoneDead=false;
    [SerializeField]bool isRevival=false;
    [SerializeField]bool isWaiting=true;

    [SerializeField]float moveSpeed=2.5f;
    public string scr="CinemachineDollyCart";//dolly track的腳本名子
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(stoneMode){
            case StoneMode.aroundMain:
                //做事
                DoAroundMain();
                //切換狀態
                if(isThrow){
                    stoneMode=StoneMode.onTheGroung;
                }
                break;
            case StoneMode.onTheGroung:
                //做事
                DoOnTheGroung();
                //切換狀態
                if(isDead){
                    stoneMode=StoneMode.dead;
                }
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
        
        // (gameObject.GetComponent(scr)as MonoBehaviour).enabled = false;
        // navMeshAgent.SetDestination(player.transform.position);
        // GetComponent<NavMeshAgent>().speed=moveSpeed;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Groung"){
            isOnTheGround=true;
        }
    }


    private void DoAroundMain(){

    }
    private void DoOnTheGroung(){
        
    }
    private void DoDead(){
        
    }
    private void DoRevival(){
        
    }
    public enum StoneMode{
        aroundMain,
        onTheGroung,
        dead,
        revival
    }
    
}
