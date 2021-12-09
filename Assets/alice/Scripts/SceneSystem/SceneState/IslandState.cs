using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandState : ISceneState
{
    
    //private Teleport teleport;
	public IslandState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "Island";
        Debug.Log("Island hello");
	}
    // 開始
	public override void StateBegin()
	{

		
	}

	// 更新
	public override void StateUpdate()
	{
		// 更換為
        //Debug.Log("now in trianing01");
		CheckChangeSceneUpdate();
		
        
	}

	
	public override void TeleportToWhichScene(){
			switch(nextSceneName){
				case "MushroomPlaza":
					m_Controller.SetState(new MushroomPlazaState(m_Controller), "MushroomPlaza");
					break;
                case "training01":
					m_Controller.SetState(new TrainingOneState(m_Controller), "Training01");


					break;
				default:
					break;
		}
	}
}
