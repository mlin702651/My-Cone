﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState
{
    private string m_StateName = "ISceneState";
    public string StateName{
        get{return m_StateName;}
        set{m_StateName = value;}
    }

    //controller
    protected SceneStateController m_Controller = null;

    //Constructor
    public ISceneState(SceneStateController Controller){
        m_Controller = Controller;
    }

    public virtual void StateBegin(){}

    public virtual void StateEnd(){}

    public virtual void StateUpdate(){}

    public override string ToString()
    {
        return string.Format ("[I_SceneState: StateName={0}]", StateName);
    }
}
