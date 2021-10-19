using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingOneState : ISceneState
{
    
    //private Teleport teleport;
	public TrainingOneState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "Training01";
        Debug.Log("training one hello");
	}
    // 開始
	public override void StateBegin()
	{

		//teleport = 
	}

	// 更新
	public override void StateUpdate()
	{
		// 更換為
        //Debug.Log("now in trianing01");

		//if(MySceneManager.instance.thisSceneIsOver){
			m_Controller.SetState(new TrainingTwoState(m_Controller), "Training02");
			//MySceneManager.instance.StartNewScene();
		//}
        
	}
}
