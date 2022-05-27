using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 
using UnityEngine.UI;



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
    bool startVideo = false;
    bool endVideo = false;
    CinemachineFreeLook freeLookCam;
    [SerializeField] float cameraShakeDuration = 2;
    [SerializeField]DialogueBase cameraEndDialogue;
    [SerializeField]DialogueBase videoEndDialogue;
    [SerializeField]GameObject legendVideo;
    [SerializeField]GameObject legendVideoCanvas;
    [SerializeField]RawImage legendVideoRenderer;
    [SerializeField]Image legendVideoBg;
    [SerializeField]GameObject teleportPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartVideo();
        freeLookCam = GameManager.instance.FreeLookCam;
    }

    // Update is called once per frame
    void Update()
    {
        if(canShake){
            canShake = false;
            ShakeCamera(10,cameraShakeDuration);
        }

        if(startVideo){
            FadeInCanvas();
        }
        if(endVideo){
            FadeOutCanvas();
        }
    }

    public void StartEvent(){
        Debug.Log("shake!");
        canShake = true;
    }

    public void StartVideo(){
        Debug.Log("video!");
        legendVideoCanvas.SetActive(true);
        legendVideo.SetActive(true);
        startVideo = true;
        FunctionTimer.Create(()=> EndVideo(), 2f);
        DialogueManager.instance.inDialogue = true;

    }

    public void ShakeCamera(float intensity , float time){
        freeLookCam = GameManager.instance.FreeLookCam;
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

        DialogueManager.instance.EnqueueDialogue(cameraEndDialogue);
        DialogueManager.instance.StartDialogue();

    }
    void EndVideo(){
        endVideo = true;
        
    }

    void FadeInCanvas(){
        var tempColor = legendVideoRenderer.color;
        var tempColor2 = legendVideoBg.color;
        tempColor.a += 0.01f;//2s
        tempColor2.a += 0.01f;//2s
        legendVideoRenderer.color = tempColor;
        legendVideoBg.color = tempColor2;
        if(tempColor.a>=1.0f){ 
            startVideo = false;
        }
    }
    void FadeOutCanvas(){
        var tempColor = legendVideoRenderer.color;
        var tempColor2 = legendVideoBg.color;
        tempColor.a -= 0.01f;//2s
        tempColor2.a -= 0.01f;//2s
        legendVideoRenderer.color = tempColor;
        legendVideoBg.color = tempColor2;
        if(tempColor.a<=0){
            endVideo = false;
            legendVideo.SetActive(false);
            legendVideoCanvas.SetActive(false);
            DialogueManager.instance.EnqueueDialogue(videoEndDialogue);
            DialogueManager.instance.StartDialogue();
            teleportPoint.SetActive(true);
            //inVideo= false;  

        }
    }
    
}
