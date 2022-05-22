using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]AudioSource clearSound;
    public PlayerStateMachine Player;
    public CinemachineFreeLook FreeLookCam;
    public bool isChangingScene;
    private void Awake(){
        if(instance== null){
            instance = this;
        }
        else{
            Destroy(gameObject);

        }

        DialogueTrigger[] triggers = FindObjectsOfType<DialogueTrigger>();
        foreach (var trigger in triggers)
        {
            allDialogueTriggers.Add(trigger);
        }
        //allDialogueTriggers = FindObjectsOfType<DialogueTrigger>();
        
    }

    public void ResetPlayerRespwan(RespawnPoint respawnPoint){
        isChangingScene = true;
        Player = FindObjectOfType<PlayerStateMachine>();
        Player.transform.position = new Vector3(respawnPoint.respawnPosition.x,respawnPoint.respawnPosition.y,respawnPoint.respawnPosition.z);
        Player.transform.rotation = respawnPoint.respawnRotation;
        StartCoroutine(EndChangeScene());
    }

    private IEnumerator EndChangeScene(){
        yield return new WaitForSeconds(3f);
        instance.isChangingScene = false;
        yield return null;
    }

    public void UpdateTrigger(){
        DialogueTrigger[] triggers = FindObjectsOfType<DialogueTrigger>();
        allDialogueTriggers.Clear();
        foreach (var trigger in triggers)
        {
            //if(!allDialogueTriggers.Contains(trigger)){
                allDialogueTriggers.Add(trigger);
            //}
        }
    }

    //public DialogueTrigger[] allDialogueTriggers;
    public List<DialogueTrigger> allDialogueTriggers = new List<DialogueTrigger>();

    public delegate void OnEnemyDeathCallBack(MonsterProfile monsterProfile);
    public OnEnemyDeathCallBack onEnemyDeathCallBack;

    public delegate void OnPlayerArrivedCallBack(string sceneName);
    public OnPlayerArrivedCallBack onPlayerArrivedCallBack;

    public void PlayAudio(){
        if(!clearSound.isPlaying){
                    clearSound.Play();
                }
    }
    
}
