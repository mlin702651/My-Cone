using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingTwoState : ISceneState
{
    
    public TrainingTwoState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "Training02";
        Debug.Log("training two hello");
	}
    // 開始
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
				case "Training03":
					m_Controller.SetState(new TrainingThreeState(m_Controller), "Training03");
					break;
				case null:
					break;
		}
	}
}
