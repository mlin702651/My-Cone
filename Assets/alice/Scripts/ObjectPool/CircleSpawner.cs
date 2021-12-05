using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
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
        if(timer>=5){
            timer = 0;
          //ObjectPooler.Instance.SpawnFromPool("Circle",transform.position,transform.rotation);
        }
    }
}
