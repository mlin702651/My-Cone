using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[System.Serializable]
public class Dialogue //擴充版
{
    public DialogueSet[] dialogueSet;
}
[System.Serializable]
public class DialogueSet{
    //public sprite? image;
    
    public Characteres chara = Characteres.Woomi;
    public enum Characteres{
        Woomi,//=0
        Woowoowawa,//=1
        Park,
        Lily,
        Bary,
        Dan,   
    }
    public string name;
    [TextArea]public string sentences;
}

//不知道可不可以直接定義一個圖片庫 不然要一直拖圖片很麻煩
//可能掛在dialogue manager那邊 傳給他一個名字 讓他回傳一個sprite 然後播的時候直接用
