using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]AudioSource clearSound;
    [SerializeField]PlayerStateMachine player;
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
        player = FindObjectOfType<PlayerStateMachine>();
        player.transform.position = new Vector3(respawnPoint.respawnPosition.x,respawnPoint.respawnPosition.y,respawnPoint.respawnPosition.z);
        player.transform.rotation = respawnPoint.respawnRotation;
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
