using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class bulletcontroller : MonoBehaviour
{
    Controls controls;
    public GameObject firePoint;
    float timer = 10;
    float flyTime = 2;
    //續力
    #region magic01
    [Header("Song Magic")]
    public GameObject startEffect01;
    GameObject cloneStart;
    public GameObject flashEffect01;
    public Rigidbody bullet01;
    public float bulletSpeed01=10.0f;
    bool accumulateSuccess = true;
    bool Flag = false;
    float pressTime = 0;
    float calculatePressToFly = 0;
    #endregion
    //泡泡
    #region magic02
    [Header("Bubble Magic")]
    public GameObject flashEffect02;
    public Rigidbody bullet02;
    public float bulletSpeed02 = 10.0f;
    GameObject cloneFlash;
    #endregion
    //炸半
    #region magic03
    [Header("Mushroom Magic")]
    public GameObject smoke;
    public GameObject bomb;
    float exploTimer=0; //炸彈炸掉的時間
    bool exploFlag = false;
    float bombTimer = 10; //炸彈炸掉的時間
    Vector3 bombPosition;
    [SerializeField]
    float magic03CDTime = 3;
    float CDTimer03 = 0;
    #endregion
    //status

    private int magicStatus = 0; //0海螺 1泡泡 2海菇
    private bool PlusMagicStatusPress = false;
    private bool MinusMagicStatusPress = false;

    //手把控制
    private bool ShootPressDown = false;
    private bool ShootPressUp = false;
    private bool isPressingShoot = false;
    

    //旋轉firepoint跟魔法 面對woomi的方向
    private bool isRotating = false;
    public Transform woomi;

    void Awake(){
        controls = new Controls();

        //射擊
        controls.player.Shoot.started += ctx => ShootStart();
        controls.player.Shoot.canceled += ctx => ShootCanceled();
        //切換魔法
        controls.player.SwitchWeaponPlus.started += ctx => PlusMagicStatus();
        controls.player.SwitchWeaponLess.started += ctx => MinusMagicStatus();
    }
    void OnEnable()
    {
        controls.player.Enable();
    }
    void OnDisable()
    {
        controls.player.Disable();
    }
    void Update()
    {
        switch (magicStatus){
                case 0: //海螺
                    magic01();
                    
                    break;
                case 1:
                    magic02();
                    break;
                case 2:
                    magic03();
                    break;
                default:
                    break;
        }
        #region ChangeMagic
        if(PlusMagicStatusPress){
            ShootPressUp = false;
            PlusMagicStatusPress = false;
            magicStatus ++;
            if(magicStatus==3) magicStatus = 0;
            Debug.Log("magicStatus"+magicStatus);
        }
        if(MinusMagicStatusPress){
            ShootPressUp = false;
            MinusMagicStatusPress = false;
            magicStatus --;
            if(magicStatus==-1) magicStatus = 2;
            Debug.Log("magicStatus"+magicStatus);
        }
        #endregion
    }

    void magic01()
    {
        Debug.Log(accumulateSuccess);
        if (cloneStart != null)
        {
            cloneStart.transform.position = transform.position;

        }
        //cloneStart.transform.position = firePoint.transform.position;
        //if (Input.GetKeyDown("q"))
        if (ShootPressDown)
        {
            ShootPressDown = false;
            pressTime = 0;
            Debug.Log("in");          
            cloneStart = Lean.Pool.LeanPool.Spawn(startEffect01, transform.position, Quaternion.identity);
            accumulateSuccess = true;
        }
        //開始記錄續力時間 
        //if (Input.GetKey("q"))
        if (isPressingShoot)
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
        //Debug.Log(pressTime);
        if (calculatePressToFly < 0.3f)
        {
            flyTime = 0.2f;
        }
        else
        {
            flyTime = Mathf.Pow(2, calculatePressToFly) - 1.0f;
        }
       
        //if (Input.GetKeyUp("q")&& accumulateSuccess)
        if (ShootPressUp&& accumulateSuccess)
        {
            ShootPressUp = false;
            Lean.Pool.LeanPool.Despawn(cloneStart);

            Flag = true;
            GameObject cloneFlash;
            cloneFlash = Lean.Pool.LeanPool.Spawn(flashEffect01, transform.position, Quaternion.identity);
            Lean.Pool.LeanPool.Despawn(cloneFlash, 2);

            Rigidbody clonebullet;
            clonebullet = Lean.Pool.LeanPool.Spawn(bullet01, transform.position, Quaternion.identity) as Rigidbody;
            clonebullet.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed01);//讓子彈飛
            //面向飛的方向
            clonebullet.transform.rotation = firePoint.transform.rotation;
            // Vector3 relativePos = woomi.position - transform.position;
            //clonebullet.transform.rotation = Quaternion.LookRotation(relativePos);

            Lean.Pool.LeanPool.Despawn(clonebullet, flyTime);

            pressTime = 0;                 
        }
    }

    void magic02()
    {
        if (cloneFlash != null)
        {
            cloneFlash.transform.position = transform.position;

        }
        //if (Input.GetKey("e"))
        if (isPressingShoot)
        {
            ShootPressUp = false;
            if (timer > 0.5f)
            {
              
                //GameObject cloneFlash;
                cloneFlash = Lean.Pool.LeanPool.Spawn(flashEffect02, transform.position, Quaternion.identity);
                Lean.Pool.LeanPool.Despawn(cloneFlash, 1);
                Rigidbody clonebullet;
                clonebullet = Lean.Pool.LeanPool.Spawn(bullet02, transform.position, Quaternion.identity) as Rigidbody;
                clonebullet.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed02);//讓子彈飛
                Lean.Pool.LeanPool.Despawn(clonebullet, 1);
                timer = 0;
                
            }
            else timer += Time.deltaTime;         
        }    
    }

    void magic03()
    {
        if (ShootPressDown&& CDTimer03> magic03CDTime)
        {
            CDTimer03 = 0;
            ShootPressDown = false;
            ShootPressUp = false;
            if (bombTimer > 10f)
            {
                //炸彈
                GameObject cloneBomb;
                cloneBomb = Lean.Pool.LeanPool.Spawn(bomb, transform.position, Quaternion.identity);
                Lean.Pool.LeanPool.Despawn(cloneBomb, 6);
                bombPosition = transform.position;
                exploTimer = 0;
                timer = 0;
                exploFlag = true;
            }        
        }
        else
        {
            exploTimer += Time.deltaTime;
            bombTimer += Time.deltaTime;
            CDTimer03 += Time.deltaTime;
        }
        if (exploTimer > 4 && exploFlag)
        {
            exploFlag = false;
            //煙
            GameObject cloneSmoke;
            cloneSmoke = Lean.Pool.LeanPool.Spawn(smoke, bombPosition, Quaternion.identity);
            Lean.Pool.LeanPool.Despawn(cloneSmoke, 2);
            exploTimer = 0;
        }
    }

    #region ChangeMagic
    void PlusMagicStatus(){
        PlusMagicStatusPress = true;
    }
    void MinusMagicStatus(){
        MinusMagicStatusPress = true;
    }
    #endregion
    void ShootStart(){
        ShootPressDown = true;
        isPressingShoot = true;
        ShootPressUp = false;
    }
    void ShootCanceled(){
        //ShootPressDown = false;
        ShootPressUp = true;
        isPressingShoot = false;
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
