using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    private void Awake(){
        
        // 切換場景不會被刪除
		GameObject.DontDestroyOnLoad( this.gameObject );	
    }
    //Scene Management
    SceneStateController m_SceneStateController = new SceneStateController();
    private void Start() {
        //m_SceneStateController.SetState(new StartState(m_SceneStateController), "SampleScene");
    }

    private void Update() {
        m_SceneStateController.StateUpdate();
    }
}
