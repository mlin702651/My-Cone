﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MySceneManager : MonoBehaviour
{
    
    public static MySceneManager instance;
    private void Awake(){
        if(instance!= null){
             //Debug.LogWarning("fix this: " + gameObject.name);
            Destroy(gameObject);
        }
        else instance = this;
        // 切換場景不會被刪除
		GameObject.DontDestroyOnLoad( this.gameObject );	
    }
    //Scene Management
    SceneStateController m_SceneStateController = new SceneStateController();
    private void Start() {
        //m_SceneStateController.SetState(new StartState(m_SceneStateController), "SampleScene");
        //m_SceneStateController.SetState(new MushroomPlazaState(m_SceneStateController), "MushroomPlaza");
        m_SceneStateController.SetState(new StartState(m_SceneStateController), "StartScene");
    }

    private void Update() {
        m_SceneStateController.StateUpdate();
    }

    public bool thisSceneIsOver {get;set;}= false;
    public string nextSceneName {get;set;} 
    public void EndThisScene(string teleportSceneName){
        thisSceneIsOver = true;
        nextSceneName = teleportSceneName;
    }
    public void StartNewScene(){
        thisSceneIsOver = false;
    }

    public SceneStateController GetCurrentController(){
        return m_SceneStateController;
    }
}
