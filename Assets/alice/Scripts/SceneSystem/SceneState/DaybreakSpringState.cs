using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaybreakSpringState : ISceneState
{
    
    //private Teleport teleport;
	public DaybreakSpringState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "DaybreakSpring";
        Debug.Log("DaybreakSpring hello");
	}
    // 開始
	public override void StateBegin()
	{
//
		
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
