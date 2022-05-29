using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField]private GameObject endingCanvas;
    [SerializeField]private RespawnPoint respawnPoint;
    private bool isUsed = false ;
    private void OnTriggerEnter(Collider other) {
        if(isUsed) return;
        if(other.tag=="Player"){
            endingCanvas.SetActive(true);
            StartCoroutine(BackToMenu(respawnPoint));
            isUsed = true;
            //Destroy(gameObject);
        }

    }

    private IEnumerator BackToMenu(RespawnPoint respawnPoint){
        yield return new WaitForSeconds(5f);
        endingCanvas.SetActive(false);
        WoomiSceneManager.instance.LoadScene("StartScene",respawnPoint);
    }
}
