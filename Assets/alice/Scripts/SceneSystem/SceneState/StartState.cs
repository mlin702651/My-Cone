using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "StartScene";
        Debug.Log("StartState hello");
        
	}

	// 開始
	public override void StateBegin()
	{
		// 可在此進行遊戲資料載入及初始...等
        Debug.Log("StartState begin");

        //m_Controller.SetState(new TrainingTwoState(m_Controller), "Training02");
	}

	// 更新
	public override void StateUpdate()
	{
		// 更換為
		CheckChangeSceneUpdate();
		
        //Debug.Log("update to trianing01");
	}

	public override void TeleportToWhichScene(){
			Debug.Log("telePort");
			switch(nextSceneName){
				case "MushroomPlaza":
					m_Controller.SetState(new MushroomPlazaState(m_Controller), "MushroomPlaza");
					break;
				case null:
					break;
		}
	}
}
