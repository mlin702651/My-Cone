using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]BigStone bigStone=null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( bigStone.getIsDead()){
            gameObject.SetActive(false);
        }
       
    }
}
