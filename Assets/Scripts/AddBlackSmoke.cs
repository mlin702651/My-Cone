using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlackSmoke : MonoBehaviour
{
    [SerializeField]BigStone bigStone=null;
    [SerializeField]StoneController stoneController;
    [SerializeField]GameObject teleportParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bigStone.GetTeleport()){
            Instantiate(teleportParticle,bigStone.getPosition());
        }
    }
}
