using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RespawnPoint", menuName = "RespawnPoint")]
public class RespawnPoint : ScriptableObject
{
    public  Vector3 respawnPosition;
    public  Quaternion respawnRotation;


    public void SetValue(Vector3 newPos, Quaternion newRot){
        respawnPosition = newPos;
        respawnRotation = newRot;
        ForceSerialization();
    }

    

    void ForceSerialization()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
    #endif
    }
}
