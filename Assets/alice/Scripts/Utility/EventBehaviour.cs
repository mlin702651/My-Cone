using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event/HatEvent")]
public class EventBehaviour : ScriptableObject
{
    public void ChangeToWitchEvent(){
        //這裡寫換成巫師帽的程式碼
        Debug.Log("換成巫師帽");
    }
}
