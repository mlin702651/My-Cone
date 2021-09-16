using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "Character Profile")]
public class CharacterProfile : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
}
