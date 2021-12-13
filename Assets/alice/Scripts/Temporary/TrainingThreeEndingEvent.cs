using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 
using Cinemachine.Editor; 
using Cinemachine.Utility;


public class TrainingThreeEndingEvent : MonoBehaviour
{
    public static TrainingThreeEndingEvent instance;
        private void Awake(){
        if(instance != null){
            Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;
    }

    bool canShake = false;
    [SerializeField] CinemachineFreeLook freeLookCam;
    [SerializeField] float cameraShakeDuration = 2;
    [SerializeField]DialogueBase endingDialogue;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canShake){
            canShake = false;
            ShakeCamera(10,cameraShakeDuration);
        }
    }

    public void StartEvent(){
        Debug.Log("shake!");
        canShake = true;
    }

    public void StartVideo(){
        Debug.Log("video!");
        
    }

    public void ShakeCamera(float intensity , float time){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
        freeLookCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        cinemachineBasicMultiChannelPerlin =
        freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        cinemachineBasicMultiChannelPerlin =
        freeLookCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        FunctionTimer.Create(()=> ResetCamera(), time);
    }

    void ResetCamera(){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
        freeLookCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();    
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

        cinemachineBasicMultiChannelPerlin =
        freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

        cinemachineBasicMultiChannelPerlin =
        freeLookCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

        DialogueManager.instance.EnqueueDialogue(endingDialogue);
        DialogueManager.instance.StartDialogue();

    }

    
}
