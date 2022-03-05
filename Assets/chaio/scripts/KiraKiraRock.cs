using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiraKiraRock : MonoBehaviour
{
    [SerializeField]GameObject rockParticle;
    [SerializeField]GameObject awardKiraRock;
    [SerializeField]GameObject copyKiraRock;
    //[SerializeField]float speed=0.5f;
    Vector3 hitPosition=new Vector3(0,0,0);
    int hitCount=0;
    [SerializeField]int itemQuantity=3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player_Magic1")
    //     {
    //         hitPosition = other.ClosestPoint(transform.position);
    //         rockParticle.transform.position=hitPosition;
    //         rockParticle.SetActive(true);
    //         Debug.Log("Point of contact: "+hitPosition);
    //         Debug.Log("hurt1!");
    //     }
    //     else if (other.tag == "Player_Magic2")
    //     {
          
    //         Debug.Log("hurt2!");
    //     }
    //     else if (other.tag == "Player_Magic3")
    //     {
    //         Physics.IgnoreCollision(other, gameObject.GetComponent<SphereCollider>());
    //         Debug.Log("hurt3!");
    //     }
    // }
    void OnCollisionEnter(Collision other) {
        if(hitCount<itemQuantity){
            if(other.gameObject.tag == "Player_Magic1"){
            hitCount+=1;
            ContactPoint contactPoint=other.contacts[0];
            Vector3 appearDir=Vector3.Reflect(transform.position,contactPoint.normal);
            copyKiraRock=Instantiate(awardKiraRock, contactPoint.point,Quaternion.FromToRotation(Vector3.up, contactPoint.normal));
            }
        }
        else{
            rockParticle.SetActive(false);
        }
        
    }
}
