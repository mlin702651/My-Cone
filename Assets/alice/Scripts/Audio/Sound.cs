using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "GameSounds")]
public class Sound : ScriptableObject
{
    
    public string soundName;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 1;
    [Range(.1f,3f)]
    public float pitch = 1;

    public bool loop;
    
    [HideInInspector]
    public AudioSource source;
    public AudioMixerGroup mixer;
}
