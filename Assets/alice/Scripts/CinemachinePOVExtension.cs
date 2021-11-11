using UnityEngine;
using Cinemachine;
public class CinemachinePOVExtension : CinemachineExtension
{
    private InputSystem inputSystem;

    protected override void Awake() {
        inputSystem = InputSystem.instance;
        base.Awake();
    }
    
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime){

    }
}
