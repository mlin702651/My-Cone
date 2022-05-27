using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Teleport : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.5f;
    [SerializeField] private string targetSceneName; 
    [SerializeField]RespawnPoint nextRespawnPoint;
    // [SerializeField]RespawnPoint currentStageRespawnPoint;
    // [SerializeField]RespawnPoint changeRespawnPoint;

    //public delegate void TeleportDelegate(RespawnPoint newResoawnPoint);
    //public static event TeleportDelegate onSceneChange;

    private bool canInteract;
    
    
    // Start is called before the first frame update
    void Start()
    {
        CapsuleCollider capsuleCollider = gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        capsuleCollider.radius = interactRange;
        capsuleCollider.isTrigger = true;
        //Debug.Log(targetScene.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract){
            canInteract = false;
            //currentStageRespawnPoint.SetValue(changeRespawnPoint.respawnPosition,changeRespawnPoint.respawnRotation);
            //currentStageRespawnPoint.respawnPosition = changeRespawnPoint.respawnPosition;
            //currentStageRespawnPoint.respawnRotation = changeRespawnPoint.respawnRotation;
            //MySceneManager.instance.EndThisScene(targetScene.name);
            //onSceneChange?.Invoke(nextRespawnPoint);
            Debug.Log(nextRespawnPoint);
            //WoomiSceneManager.instance.LoadScene(targetScene.name,nextRespawnPoint);
            WoomiSceneManager.instance.LoadScene(targetSceneName,nextRespawnPoint);
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
