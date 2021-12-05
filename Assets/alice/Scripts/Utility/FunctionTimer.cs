using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    
    private static List<FunctionTimer> _activeTimerList;
    private static GameObject _initGameObject;
    private static void InitIfNeeded(){
        if(_initGameObject  == null){
            _initGameObject = new GameObject("FunctionTimer_InitGameObject");
            _activeTimerList = new List<FunctionTimer>();
        }
    }
    
    public static FunctionTimer Create(Action action, float timer,string timerName = null){
        InitIfNeeded();
        
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, timerName, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;
        
        _activeTimerList.Add(functionTimer);

        return functionTimer;
    }

    public static void RemoveTimer(FunctionTimer functionTimer){
        InitIfNeeded();
        _activeTimerList.Remove(functionTimer);
    }

    public static void StopTimer(string timerName){
        for(int i=0; i< _activeTimerList.Count; i++){
            if(_activeTimerList[i]._timerName == timerName){
                //stop the timer
                _activeTimerList[i].DestroySelf();
                i--;
            }
        }
    }


    //class to have access to Monobehaviour functions
    public class MonoBehaviourHook : MonoBehaviour{
        public Action onUpdate;
        private void Update() {
            if(onUpdate != null) onUpdate();
        }
    }
    private Action _action;
    private float _timer;
    private string _timerName;
    private GameObject _gameObject;
    private bool _isDestroyed;
    private FunctionTimer(Action action, float timer, string timerName, GameObject gameObject){
        _action = action;
        _timer = timer;
        _timerName = timerName;
        _gameObject = gameObject;
        _isDestroyed = false;
    }
    public void Update(){
        if(!_isDestroyed){
            _timer -= Time.deltaTime;
            if(_timer<0){
                //Trigger the action
                _action();
                DestroySelf();
                _isDestroyed = false;
            }
        }
    }

    private void DestroySelf(){
        _isDestroyed = true;
        UnityEngine.Object.Destroy(_gameObject);
        RemoveTimer(this);
    }
    
    //usage
    //FunctionTimer.Create(() => testAction, time);
}
