using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic01 : MonoBehaviour
{
    public float speed=3.0f ;
    public float rate;
    ParticleSystem ps01;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }
}
