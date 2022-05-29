using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private MonsterProfile monsterProfile;
    [SerializeField]private GameObject[] recoveries;
    [SerializeField]BigStone bigStone=null;
    // Update is called once per frame

    private void Start() {
        //StartCoroutine(IdontWantToFight());
    }
    void Update()
    {
        
        if( bigStone.getIsDead()){
            if(GameManager.instance.onEnemyDeathCallBack != null) GameManager.instance.onEnemyDeathCallBack.Invoke(monsterProfile);
            foreach (var recovery in recoveries)
            {
                recovery.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator IdontWantToFight(){
        yield return new WaitForSeconds(7f);
        if(GameManager.instance.onEnemyDeathCallBack != null) GameManager.instance.onEnemyDeathCallBack.Invoke(monsterProfile);
        foreach (var recovery in recoveries)
        {
            recovery.SetActive(true);
        }
        Destroy(gameObject);
    }
}
