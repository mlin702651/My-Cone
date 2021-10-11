using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "Character Profile")]
public class CharacterProfile : ScriptableObject
{
    public string characterName;
    public Sprite characterImage_Normal;
    public Sprite characterImage_Happy;
    public Sprite characterImage_Mad;
    public Sprite characterImage_Ok;
    public Sprite characterImage_Scare;
    public Sprite characterImage_Shock;
}
