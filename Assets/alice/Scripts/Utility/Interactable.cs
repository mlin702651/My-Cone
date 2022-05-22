using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float interactRange = 1.5f;
    //[SerializeField]private Image interactHint;
    protected bool canInteract;

    Controls controls;
    private bool ConversationPress = false;


    void Awake(){
        controls = new Controls();
         //對話
            controls.player.Talk.started += ctx => ConversationStart();
            controls.player.Talk.canceled += ctx => ConversationCanceled();
    }

    void OnEnable()
    {
        controls.player.Enable();
    }
    void OnDisable()
    {
        controls.player.Disable();
    }
    public virtual void Start()
    {
        CapsuleCollider capsuleCollider = gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        capsuleCollider.radius = interactRange;
        capsuleCollider.isTrigger = true;
        //interactHint.DOFade(0,0f);

    }

    public virtual void Update() {
        if(canInteract){
            if(ConversationPress){
                canInteract = false;
                ConversationPress = false;
                Interact();
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            //interactHint.DOFade(1,0.5f);//換成ui顯示
            Debug.Log(other.name);
            Debug.Log("NPCTalk");
            UIGuide.instance.uiNPCTalk.SetActive(true);
            canInteract = true;
        }
    }
    

    private void OnTriggerStay(Collider other) {
        
    }

    public virtual void OnTriggerExit(Collider other) {
       // interactHint.DOFade(0,0.3f);//換成ui顯示
        UIGuide.instance.uiNPCTalk.SetActive(false);
        canInteract = false;
    }

    public virtual void Interact(){

    }

    void ConversationStart(){
            //Debug.Log("ConversationBtn Start");
            ConversationPress = true;
    }
    void ConversationCanceled(){
            //Debug.Log("ConversationBtn Leave");
            ConversationPress = false;


    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
