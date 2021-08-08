using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTransparentFollowPlayer : MonoBehaviour
{
    //取得material 變數的Id
    int posID=Shader.PropertyToID("_Position");
    int sizeID=Shader.PropertyToID("_Size");

    public Material wallMaterial;
    public Camera cerrentCamera;
    public LayerMask mask;

    //用來調整圓中心點
    [SerializeField]
    Vector3 adjustPos=new Vector3(0,0,0);

    // Update is called once per frame
    void Update()
    {
        var dir = cerrentCamera.transform.position-transform.position;
        var ray =new Ray(transform.position,dir.normalized);

        //用一個射線偵測有沒有打到牆壁 
        if(Physics.Raycast(ray,3000,mask))
        {
            wallMaterial.SetFloat(sizeID,1.5f);
        }
        else
        {
            wallMaterial.SetFloat(sizeID,0);
        }
        
        //把位子丟給material
        var view = cerrentCamera.WorldToViewportPoint(transform.position)+ adjustPos;
        wallMaterial.SetVector(posID,view);
    }
}
