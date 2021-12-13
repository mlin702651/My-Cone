using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]AudioSource clearSound;
    private void Awake(){
        if(instance== null){
            instance = this;
        }

        allDialogueTriggers = FindObjectsOfType<DialogueTrigger>();
        
    }

    public DialogueTrigger[] allDialogueTriggers;

    public delegate void OnEnemyDeathCallBack(MonsterProfile monsterProfile);
    public OnEnemyDeathCallBack onEnemyDeathCallBack;

    public void PlayAudio(){
        if(!clearSound.isPlaying){
                    clearSound.Play();
                }
    }
    
}
