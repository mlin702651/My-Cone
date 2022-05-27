using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class WoomiSceneManager : MonoBehaviour
{
    public static WoomiSceneManager instance;
    [SerializeField]private GameObject loadingCanvas;
    public delegate void SceneManagerDelegate(RespawnPoint newResoawnPoint);
    public static event SceneManagerDelegate onSceneChange;
    private void Awake() {
        if(instance== null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            //Debug.Log("There might be two SceneManager");
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string newSceneName, RespawnPoint newRespawnPoint){
        var scene = SceneManager.LoadSceneAsync(newSceneName);
        scene.allowSceneActivation = false;


        loadingCanvas.SetActive(true);

        do{
            await Task.Delay(300);
            //progressbar.fillAmount = scene.progress;
        }while(scene.progress < 0.9f);
        Debug.Log(newRespawnPoint);

        if(newRespawnPoint!=null){
            GameManager.instance.ResetPlayerRespwan(newRespawnPoint);
        }
        await Task.Delay(100);
        scene.allowSceneActivation = true;
        await Task.Delay(3000);
        loadingCanvas.SetActive(false);
        await Task.Delay(1000);
        GameManager.instance.onPlayerArrivedCallBack?.Invoke(newSceneName);
        GameManager.instance.UpdateTrigger();
        //GameManager.instance.ResetPlayer();
    }

    public async void ReloadScene(){
        string sceneToReload = SceneManager.GetActiveScene().name;
        var scene = SceneManager.LoadSceneAsync(sceneToReload);
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);

        do{
            await Task.Delay(300);
            //progressbar.fillAmount = scene.progress;
        }while(scene.progress < 0.9f);

        await Task.Delay(100);
        scene.allowSceneActivation = true;
        await Task.Delay(3000);
        loadingCanvas.SetActive(false);
    }
}
