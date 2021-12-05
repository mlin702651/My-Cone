﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer>=1f){
            timer = 0;
          //ObjectPooler.Instance.SpawnFromPool("Rain",transform.position,transform.rotation);
        }
    }
}
