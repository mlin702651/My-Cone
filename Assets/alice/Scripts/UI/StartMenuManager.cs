using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartMenuManager : MonoBehaviour
{
    [SerializeField]GameObject[] icons = new GameObject[4];
    private int currentSelection = 0;

    // Update is called once per frame
    void Update()
    {
        if(InputSystem.instance.MenuSelectUpPressDown){
            InputSystem.instance.MenuSelectUpPressDown = false;
            currentSelection = (currentSelection > 0) ? currentSelection-1 : currentSelection;
            SetIcon(currentSelection);
            //Debug.Log("up");
        }
        if(InputSystem.instance.MenuSelectDownPressDown){
            InputSystem.instance.MenuSelectDownPressDown = false;
            currentSelection = (currentSelection < 3) ? currentSelection+1 : currentSelection;
            SetIcon(currentSelection);
            //Debug.Log("down");
        }

        if(currentSelection==0 && InputSystem.instance.MenuConfirmPressDown){
            InputSystem.instance.MenuConfirmPressDown = false;
            Debug.Log("ChangeScene");
            //MySceneManager.instance.EndThisScene("MushroomPlaza");
            WoomiSceneManager.instance.LoadScene("island",null);
        }
    }

    void SetIcon(int toOpen){
        foreach (GameObject icon in icons) //先把四個都關掉
        {
            icon.SetActive(false);

        }
        icons[toOpen].SetActive(true);
    }


}
