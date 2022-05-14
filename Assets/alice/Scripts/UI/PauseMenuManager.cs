using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    
    
    public static PauseMenuManager instance;
    private void Awake(){
        if(instance!= null){
             //Debug.LogWarning("fix this: " + gameObject.name);
            Destroy(gameObject);
        }
        else instance = this;
    }
    [SerializeField] private GameObject PauseMenuUI;
    public bool inPauseMenu = false;
    // Update is called once per frame
    void Update()
    {
        if(InputSystem.instance.MenuPressDown){
            InputSystem.instance.MenuPressDown = false;
            PauseMenuUI.SetActive(!PauseMenuUI.activeSelf);
            Time.timeScale = PauseMenuUI.activeSelf? 0f:1f;
            inPauseMenu = PauseMenuUI.activeSelf;
        }
    }
}
