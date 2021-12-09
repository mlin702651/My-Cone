using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingThreeState : ISceneState
{
    
    public TrainingThreeState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "Training03";
        Debug.Log("training three hello");
	}


    
   
    
    public override void StateBegin()
	{
		// 可在此進行遊戲資料載入及初始...等
        //m_Controller.SetState(new TrainingOneState(m_Controller), "Training01");

	}

	// 更新
	public override void StateUpdate()
	{
		// 更換為
        //Debug.Log("now in trianing02");
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
