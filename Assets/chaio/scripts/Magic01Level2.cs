using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic01Level2 : MonoBehaviour
{
    [SerializeField]float speed=0.5f;
    float timer=0;
    bool start=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if(start){
             transform.position+=transform.forward*speed*Time.deltaTime;
        }
       
       
        
    }
     void OnTriggerEnter(Collider other)
        {
            start=true;
        }
}
