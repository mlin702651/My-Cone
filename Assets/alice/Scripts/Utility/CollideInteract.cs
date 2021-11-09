using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideInteract : MonoBehaviour
{
    
    public bool isInteract = false;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            isInteract = true;
        }
    }
}
