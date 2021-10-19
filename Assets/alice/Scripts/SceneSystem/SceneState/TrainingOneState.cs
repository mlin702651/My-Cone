using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingOneState : ISceneState
{
    
    public TrainingOneState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "Training01";
        Debug.Log("training one hello");
	}
    // 開始
	public override void StateBegin()
	{
		// 可在此進行遊戲資料載入及初始...等
	}

	// 更新
	public override void StateUpdate()
	{
		// 更換為
		
        Debug.Log("now in trianing01");
        
	}
}
