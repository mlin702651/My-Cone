using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.5f;
    private bool canInteract;
    
    
    // Start is called before the first frame update
    void Start()
    {
        CapsuleCollider capsuleCollider = gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        capsuleCollider.radius = interactRange;
        capsuleCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract){
            canInteract = false;
            //MySceneManager.instance.EndThisScene(new TrainingTwoState(MySceneManager.instance.GetCurrentController()), "Training02");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            canInteract = true;
        }
    }
    

    private void OnTriggerStay(Collider other) {
        
    }

    private void OnTriggerExit(Collider other) {
        canInteract = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
