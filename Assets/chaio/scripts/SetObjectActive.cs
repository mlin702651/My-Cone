using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectActive : MonoBehaviour
{
     [SerializeField] AudioSource audioData;
    [SerializeField]float activeTime=3.23f;
    float timer=0;
    // Start is called before the first frame update
    void Start()
    {
        audioData= GetComponent<AudioSource>();
        audioData.PlayDelayed(activeTime);
        timer=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>4){
            audioData.Stop();
            timer=0;
        }
        else{
            timer+=Time.deltaTime;
        }
   
        
    }
}
