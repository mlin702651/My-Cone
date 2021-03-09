using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class bulletExplore : MonoBehaviour
{
    public GameObject exp1;
   
    void OnCollisionEnter(Collision other)
    {
        GameObject particle1;
        particle1=Instantiate(exp1, transform.position, transform.rotation);
        Destroy(particle1, 1);
        Lean.Pool.LeanPool.Despawn(gameObject);

    }
}
