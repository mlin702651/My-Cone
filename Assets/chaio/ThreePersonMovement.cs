using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform mainCam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity ;

    //aim
    public Transform targetAim;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    //bool switchCam = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //瞄準相機的移動
        if (Input.GetKey("space"))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            Debug.Log("mouseX"+ mouseX);
            Debug.Log("mouseY" + mouseY);
            //xRotation -= mouseY;
            //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //aimCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //targetAim.transform.position += new Vector3(0, mouseY, 0);
            controller.transform.Rotate(Vector3.up * mouseX);

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * speed * Time.deltaTime);
        }
        //free相機的移動
        else
        {
            if (direction.magnitude >= 0.1f){
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
        



    }
}
