using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class bulletcontroller : MonoBehaviour
{
    public GameObject firePoint;
    public Rigidbody bullet;
    public float bulletSpeed=10.0f;

    Vector3 bulletPos ;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            //創造一個子彈
            Rigidbody clone1;
            clone1= Lean.Pool.LeanPool.Spawn(bullet,transform.position, Quaternion.identity) as Rigidbody;
            clone1.velocity= transform.TransformDirection(Vector3.left* bulletSpeed);//讓子彈飛

            //clone1.AddForce(-transform.right * bulletSpeed, ForceMode.VelocityChange);
            
        }
        
    }

   
}
