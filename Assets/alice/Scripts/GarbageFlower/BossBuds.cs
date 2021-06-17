
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuds : MonoBehaviour {
    
    public GameObject leftBud;
    public GameObject rightBud;
    public float xAngle, yAngle, zAngle;
    private float _fbudsRotateZ;
    private float _fbudsRotateX;
    private float _fbudsRotateY;
    private float _fbudstimer=0;
    private bool _bbuddirection = true;
    private void Start() {
        _fbudsRotateZ = zAngle;
        _fbudsRotateX = xAngle;
        _fbudsRotateY = yAngle;
    }
    private void Update() {
        //Debug.Log("hi?");
        _fbudstimer+=Time.deltaTime;
        _fbudsRotateZ += Random.Range(-0.03f, 0.03f);
        if(_fbudstimer>=5f){
            _fbudstimer = 0;
            _bbuddirection = !_bbuddirection;
            Debug.Log("Change");
        }
        if(_bbuddirection){
            leftBud.transform.Rotate(_fbudsRotateX, _fbudsRotateY, _fbudsRotateZ, Space.Self);
            rightBud.transform.Rotate(-_fbudsRotateX, -_fbudsRotateY, -_fbudsRotateZ, Space.Self);
            //Debug.Log(leftBud.transform.rotation.z);
        }
        else{
            leftBud.transform.Rotate(-_fbudsRotateX,-_fbudsRotateY, -_fbudsRotateZ, Space.Self);
            rightBud.transform.Rotate(_fbudsRotateX, _fbudsRotateY, _fbudsRotateZ, Space.Self);
            //Debug.Log("back?");
        }
        _fbudsRotateZ = zAngle;
        _fbudsRotateX = xAngle;
        _fbudsRotateY = yAngle;
      
       
    }
}