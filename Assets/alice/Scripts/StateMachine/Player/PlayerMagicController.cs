using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicController : MonoBehaviour
{
    public static PlayerMagicController instance;
    [SerializeField]private GameObject _firePoint;
    private ObjectPooler _magicPooler;
    //[Header("Magic Conch")]
    private float _magicConchBornTime = 2;
    public float MagicConchBornTime{get{return _magicConchBornTime;} set{_magicConchBornTime = value;}}
    
    
    
    private void Awake(){
        if(instance != null){
            //Debug.LogWarning("fix this: " + gameObject.name);
            Destroy(gameObject);
        }
        else instance = this;
    }
    
    void Start()
    {
        _magicPooler = ObjectPooler.Instance;
        
    }
    
    public void MagicConchStart(){
        //AudioManager.instance.PlaySound("magicConchAccumulate");
        _magicPooler.SpawnFromPool("MagicConchStart",_firePoint.transform.position,_firePoint.transform.eulerAngles);
        _magicPooler.MakeChild("MagicConchStart", ref _firePoint);
    }

    public void CanceledMagicConch(){
        //AudioManager.instance.StopSound("magicConchAccumulate");
        _magicPooler.DisableObject("MagicConchStart");
    }

    public void MagicConchFinished(){
        //AudioManager.instance.PlaySound("magicConchShoot");
        _magicPooler.SpawnFromPool("MagicConchFlash",_firePoint.transform.position,_firePoint.transform.eulerAngles);
        _magicPooler.SpawnFromPool("MagicConchBullet",_firePoint.transform.position,_firePoint.transform.eulerAngles);

    }

    public float CaculateMagicConchBulletBornTime(float bornTime){
        return bornTime;
    }

    public void MagicBubbleStart(){
        //AudioManager.instance.PlaySound("magicBubbleShoot");
        _magicPooler.SpawnFromPool("MagicBubbleBullet",_firePoint.transform.position,_firePoint.transform.eulerAngles);
        _magicPooler.SpawnFromPool("MagicBubbleFlash",_firePoint.transform.position,_firePoint.transform.eulerAngles);
        _magicPooler.MakeChild("MagicBubbleFlash", ref _firePoint);
    }

    public void MagicBombStart(){
        //AudioManager.instance.PlaySound("magicBombShoot");
        _magicPooler.SpawnFromPool("MagicBomb",_firePoint.transform.position-new Vector3(0,0.4f,0),_firePoint.transform.eulerAngles);
        
    }
    
}
