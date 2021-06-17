using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
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
        if(timer>=0.5){
            timer = 0;
          //ObjectPooler.Instance.SpawnFromPool("Cube",transform.position,transform.rotation);
        }
    }
}
