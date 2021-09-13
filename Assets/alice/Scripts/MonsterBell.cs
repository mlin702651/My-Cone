using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MonsterBell : MonoBehaviour
{
    // Start is called before the first frame update
    #region
    #endregion
    public MonsterProfile monsterProfile;
    
    #region Animation
    [SerializeField]private GameObject leaf;
    [SerializeField]private float FloatingDuration;
    [SerializeField]private float FloatingPosition;
    [SerializeField]private float LeafRotateDuration = 2;
    [SerializeField]private float DieDistance = 2;
    [SerializeField]private float DieDuration = 5;
    [SerializeField] private Ease FloatingUpEase = Ease.Linear;
    [SerializeField] private Ease FloatingDownEase = Ease.Linear;
    [SerializeField] private Ease LeafRotateEase = Ease.Linear;
    Sequence BodyMove;
    Sequence LeafMove;
    Sequence DieMove;
    private Vector3 originalPosition;
    private Vector3 currentPosition;
    private Vector3 currentRotation;
    #endregion
    #region Health
    [SerializeField] private bool IsAlive = true;
    [Range(0,100),SerializeField] private int Hp = 100;
    
    #endregion
    
    
    void Start()
    {
         
        originalPosition = transform.position;
        BodyMove = DOTween.Sequence();
        LeafMove = DOTween.Sequence();
        DieMove = DOTween.Sequence();

        IdleAnimation();
        
    }

    // Update is called once per frame
    void Update()
    {
       if(!IsAlive) return;

       if(Hp<=0){
            IsAlive = false;
            DieAnimation();
            if(GameManager.instance.onEnemyDeathCallBack != null) GameManager.instance.onEnemyDeathCallBack.Invoke(monsterProfile); //死掉的時候會傳怪物資訊過去
       }
       
        
       
    }

    void MonsterDie(){
         Destroy(gameObject);
        Debug.Log("Die!");
    }

    void IdleAnimation(){
        
         BodyMove.Append(transform.DOMoveY(originalPosition.y+FloatingPosition, FloatingDuration).SetEase(FloatingUpEase))
         .Append(transform.DOMoveY(originalPosition.y, FloatingDuration).SetEase(FloatingDownEase));
         BodyMove.SetLoops(-1);

         LeafMove.Append(leaf.transform.DORotate(new Vector3(leaf.transform.rotation.x,leaf.transform.rotation.y,leaf.transform.rotation.z+1080)
         ,LeafRotateDuration
         ,RotateMode.LocalAxisAdd
         ).SetEase(LeafRotateEase));
         LeafMove.SetLoops(-1,LoopType.Incremental);
    }
    void DieAnimation(){
        BodyMove.Kill(true);
        currentPosition = transform.position;
        currentRotation = new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z);
        LeafMove.Kill(true);
        DieMove.Append(transform.DORotate(currentRotation+new Vector3(0,0,10*Random.Range(-8,8)),DieDuration,RotateMode.LocalAxisAdd).OnComplete(()=>MonsterDie()));
        DieMove.Insert(0,transform.DOMoveY(currentPosition.y-DieDistance,DieDuration));
    }

    private void OnTriggerEnter(Collider other) {
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
        Debug.Log("in!");

    }
}
