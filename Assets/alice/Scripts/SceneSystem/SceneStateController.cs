using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
   private ISceneState m_State;
   private bool m_bRunBegin = false;

   private static AsyncOperation asyncOperation;

    public SceneStateController(){
    }

    public void SetState(ISceneState State, string LoadSceneName){
        m_bRunBegin = false;

        //load next scene
        LoadScene(LoadSceneName);

        //end previous scene
        if(m_State != null){
            m_State.StateEnd();
        }
        m_State = State;

    }

    //loading scene
    private void LoadScene(string LoadSceneName){
        if(LoadSceneName == null || LoadSceneName.Length == 0){
            return;
        }
        SceneManager.LoadScene(LoadSceneName);
    }

    //update the scene
    public void StateUpdate(){
        asyncOperation = SceneManager.LoadSceneAsync(m_State.StateName);
        if(asyncOperation.isDone) {
            Debug.Log("still loading");
            return; //if still loading then return
        }

        //new state begin
        if(m_State != null && m_bRunBegin == false){
            m_State.StateBegin();
            m_bRunBegin = true;
        }

        if(m_State != null)
            m_State.StateUpdate();
    }





}
