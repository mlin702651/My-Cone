﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingThreeState : ISceneState
{
    
    public TrainingThreeState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "Training03";
        Debug.Log("training one hello");
	}


    
   
    
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
