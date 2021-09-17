using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Basic Dialogue")]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class DialogueSet {
        public CharacterProfile character;
        [TextArea(4,8)]
        public string content;
    }
    [Header("Insert Dialogue Content")]
    public DialogueSet[] dialogueSet;

}


