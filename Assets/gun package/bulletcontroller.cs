using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class bulletcontroller : MonoBehaviour
{
    public GameObject firePoint;
    float timer = 10;
    float flyTime = 2;
    //續力
    public GameObject startEffect01;
    GameObject cloneStart;
    public GameObject flashEffect01;
    public Rigidbody bullet01;
    public float bulletSpeed01=10.0f;
    bool accumulateSuccess = true;
    bool Flag = false;
    float pressTime = 0;
    float calculatePressToFly = 0;


    //泡泡
    public GameObject flashEffect02;
    public Rigidbody bullet02;
    public float bulletSpeed02 = 10.0f;

    //炸半
    public GameObject smoke;
    public GameObject bomb;
    float exploTimer=0;
    bool exploFlag = false;
    float bombTimer = 10;
    void Update()
    {
        magic01();
        magic02();
        magic03();
    }

    void magic01()
    {
        Debug.Log(accumulateSuccess);
        if (Input.GetKeyDown("q"))
        {
            pressTime = 0;
            Debug.Log("in");          
            cloneStart = Lean.Pool.LeanPool.Spawn(startEffect01, transform.position, Quaternion.identity);
            accumulateSuccess = true;
        }
        //開始記錄續力時間 
        if (Input.GetKey("q"))
        {
            pressTime += Time.deltaTime;
            calculatePressToFly = pressTime;
            if (pressTime >= 2.5f)
            {
                Lean.Pool.LeanPool.Despawn(cloneStart);
                accumulateSuccess = false;
              
            }
            else if (pressTime >= 1.0f)
            {
                calculatePressToFly = 1;
            }    
        }
        Debug.Log(pressTime);
        if (calculatePressToFly < 0.3f)
        {
            flyTime = 0.2f;
        }
        else
        {
            flyTime = Mathf.Pow(2, calculatePressToFly) - 1.0f;
        }
       
        if (Input.GetKeyUp("q")&& accumulateSuccess)
        {
            Lean.Pool.LeanPool.Despawn(cloneStart);

            Flag = true;
            GameObject cloneFlash;
            cloneFlash = Lean.Pool.LeanPool.Spawn(flashEffect01, transform.position, Quaternion.identity);
            Lean.Pool.LeanPool.Despawn(cloneFlash, 2);

            Rigidbody clonebullet;
            clonebullet = Lean.Pool.LeanPool.Spawn(bullet01, transform.position, Quaternion.identity) as Rigidbody;
            clonebullet.velocity = transform.TransformDirection(Vector3.left * bulletSpeed01);//讓子彈飛
            Lean.Pool.LeanPool.Despawn(clonebullet, flyTime);

            pressTime = 0;                 
        }
    }

    void magic02()
    {
        if (Input.GetKey("e"))
        {
            
            if (timer > 0.5f)
            {
              
                GameObject cloneFlash;
                cloneFlash = Lean.Pool.LeanPool.Spawn(flashEffect02, transform.position, Quaternion.identity);
                Lean.Pool.LeanPool.Despawn(cloneFlash, 1);
                Rigidbody clonebullet;
                clonebullet = Lean.Pool.LeanPool.Spawn(bullet02, transform.position, Quaternion.identity) as Rigidbody;
                clonebullet.velocity = transform.TransformDirection(Vector3.left * bulletSpeed02);//讓子彈飛
                Lean.Pool.LeanPool.Despawn(clonebullet, 1);
                timer = 0;
                
            }
            else timer += Time.deltaTime;         
        }    
    }

    void magic03()
    {
       
        if (Input.GetKeyDown("r"))
        {
            
            if (bombTimer > 10f)
            {
                //炸彈
                GameObject cloneBomb;
                cloneBomb = Lean.Pool.LeanPool.Spawn(bomb, transform.position, Quaternion.identity);
                Lean.Pool.LeanPool.Despawn(cloneBomb, 6);

                exploTimer = 0;
                timer = 0;
                exploFlag = true;
            }        
        }
        else
        {
            exploTimer += Time.deltaTime;
            bombTimer += Time.deltaTime;
        }
        if (exploTimer > 4 && exploFlag)
        {
            exploFlag = false;
            //煙
            GameObject cloneSmoke;
            cloneSmoke = Lean.Pool.LeanPool.Spawn(smoke, transform.position, Quaternion.identity);
            Lean.Pool.LeanPool.Despawn(cloneSmoke, 2);
            exploTimer = 0;
        }
    }
}

//Lean.Pool.LeanPool.Despawn(gameObject);

//GameObject cloneFlash;
//cloneFlash = Lean.Pool.LeanPool.Spawn(flashEffect02, transform.position, Quaternion.identity);
//Lean.Pool.LeanPool.Despawn(cloneFlash, 2);

//Rigidbody clonebullet;
//clonebullet = Lean.Pool.LeanPool.Spawn(bullet01, transform.position, Quaternion.identity) as Rigidbody;
//clonebullet.velocity = transform.TransformDirection(Vector3.left * bulletSpeed02);//讓子彈飛
//Lean.Pool.LeanPool.Despawn(clonebullet, 1);
