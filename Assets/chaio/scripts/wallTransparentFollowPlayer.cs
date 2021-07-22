using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTransparentFollowPlayer : MonoBehaviour
{
    public int posID=Shader.PropertyToID("_Position");
    public int sizeID=Shader.PropertyToID("_Size");

    public Material wallMaterial;
    public Camera cerrentCamera;
    public LayerMask mask;

    [SerializeField]
    Vector3 adjustPos=new Vector3(0,0,0);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = cerrentCamera.transform.position-transform.position;
        var ray =new Ray(transform.position,dir.normalized);

        if(Physics.Raycast(ray,3000,mask))
        {
            wallMaterial.SetFloat(sizeID,1.5f);
        }
        else
        {
            wallMaterial.SetFloat(sizeID,0);
        }

        var view = cerrentCamera.WorldToViewportPoint(transform.position)+ adjustPos;
        wallMaterial.SetVector(posID,view);
    }
}
