using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPlazaState : ISceneState
{
    
    //private Teleport teleport;
	public MushroomPlazaState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "MushroomPlaza";
        Debug.Log("MushroomPlaza hello");
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
				case "island":
					m_Controller.SetState(new IslandState(m_Controller), "island");
					break;
				case null:
					break;
		}
	}
}
