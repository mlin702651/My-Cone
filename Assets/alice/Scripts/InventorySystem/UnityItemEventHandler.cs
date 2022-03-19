using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityItemEventHandler : MonoBehaviour
{
    [HideInInspector] public UnityEvent unityEvent;

    public void InitiateEvent(){
        if(unityEvent!= null) unityEvent.Invoke();
    }
}
