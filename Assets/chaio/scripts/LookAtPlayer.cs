using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    float timer=0;
    [SerializeField]private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        timer=0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        //timer+=Time.deltaTime;
        
    }
}
