using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject aimCamera;
    //public GameObject aimReticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        else
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);

        }
    }
}
