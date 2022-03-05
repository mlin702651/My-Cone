using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMovement : MonoBehaviour
{
    [SerializeField]float speed=0.5f;
    bool isgrounded=false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       //if(!isgrounded)
        rb.AddForce(-transform.up*speed);
        //rb.velocity=transform.forward*speed;
        //transform.Translate(-transform.up*speed*Time.deltaTime);
    }
}
