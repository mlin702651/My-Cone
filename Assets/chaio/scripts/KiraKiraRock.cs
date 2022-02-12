using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiraKiraRock : MonoBehaviour
{
    [SerializeField]GameObject rockParticle;
    Vector3 hitPosition=new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Magic1")
        {
            hitPosition = other.ClosestPoint(transform.position);
            rockParticle.transform.position=hitPosition;
            rockParticle.SetActive(true);
            Debug.Log("Point of contact: "+hitPosition);
            Debug.Log("hurt1!");
        }
        else if (other.tag == "Player_Magic2")
        {
          
            Debug.Log("hurt2!");
        }
        else if (other.tag == "Player_Magic3")
        {
            Physics.IgnoreCollision(other, gameObject.GetComponent<SphereCollider>());
            Debug.Log("hurt3!");
        }
        
    }
}
