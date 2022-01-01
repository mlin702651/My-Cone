using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RespawnPoint", menuName = "RespawnPoint")]
public class RespawnPoint : ScriptableObject
{
    public  Vector3 respawnPosition;
    public  Quaternion respawnRotation;
}
