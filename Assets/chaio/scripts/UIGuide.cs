using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuide : MonoBehaviour
{
    public GameObject uiNPCTalk;
    public static UIGuide instance;
    private void Awake() {
        uiNPCTalk.SetActive(false);
        if(instance==null){
            instance=this;
        }
        else{
            Destroy(gameObject);
        }
    }
    //public bool isOpen = false;
    // private void Update() {
    //     if(isOpen){
    //         uiNPCTalk.SetActive(true);
    //     }
    //     else{
    //         uiNPCTalk.SetActive(false);
    //     }
    // }
    
}
