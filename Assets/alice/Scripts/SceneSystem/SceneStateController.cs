using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
   private ISceneState m_State;
   private bool m_bRunBegin = false;

   private static AsyncOperation asyncOperation;

   private bool _isFirst = false;

    public SceneStateController(){
    }

    public void SetState(ISceneState State, string LoadSceneName){
        
        m_bRunBegin = false;

        //load next scene
        //if(currentScene.name != LoadSceneName)LoadScene(LoadSceneName);
        LoadScene(LoadSceneName);

        //end previous scene
        if(m_State != null){
            m_State.StateEnd();
        }
        m_State = State;

    }

    //loading scene
    private void LoadScene(string LoadSceneName){
        Scene currentScene = SceneManager.GetActiveScene();
        if(LoadSceneName == null || LoadSceneName.Length == 0 ||currentScene.name == LoadSceneName){
            _isFirst = true;
            return;
        }
        //SceneManager.LoadScene(LoadSceneName);
        asyncOperation = SceneManager.LoadSceneAsync(LoadSceneName);
        _isFirst = false;
        //asyncOperation.allowSceneActivation = false;
    }

    //update the scene
    public void StateUpdate(){
        
        // if(asyncOperation.progress>=0.9f){
        //     asyncOperation.allowSceneActivation = true;
        // }
        //if(Application.isLoadingLevel) {
        if(!_isFirst){
            if(!asyncOperation.isDone) {
                Debug.Log("still loading");
                //Debug.Log(asyncOperation.progress);//load場景的進度
                return; //if still loading then return
            }
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
