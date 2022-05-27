using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveQuestTrigger : MonoBehaviour
{
    [SerializeField]private string targetName;
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            GameManager.instance.onPlayerArrivedCallBack?.Invoke(targetName);
            Destroy(gameObject);
        }
    }
}
