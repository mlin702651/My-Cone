using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [Header("Sounds that will always be used.")]
    public Sound[] sounds;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null){
            Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;
        
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start(){
        //PlaySound(sounds[4]);
        //FunctionTimer.Create(()=>PlaySound(sounds[0]),0.5f);
        //FunctionTimer.Create(()=>StopSound(sounds[0]),5.0f);
    }

    public void PlaySound(string name){
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        if(s == null) {
            Debug.Log("Sound:" + name + "not found!");
            return;
        }
        s.source.Play();
    }

    public void PlaySound(Sound tempSound,GameObject soundParent){
        Sound s = tempSound;
        if(soundParent.GetComponent<AudioSource>() != null)s.source = soundParent.GetComponent<AudioSource>();
        else s.source = soundParent.AddComponent<AudioSource>();
        s.source.outputAudioMixerGroup = s.mixer;
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;

        
        if(s == null) {
            Debug.Log("Sound:" + s.soundName + "not found!");
            return;
        }
        s.source.Play(); //source是一個AudioSource
        Debug.Log("play.");
        //再寫一種是傳Sound進去ㄉ 這樣就可以把那關才需要的聲音播出來
    }

    public void StopSound(Sound soundToStop){
        Sound s = soundToStop;
        if(s == null) {
            Debug.Log("Sound:" + s.soundName + "not found!");
            return;
        }
        s.source.Stop(); //source是一個AudioSource
    }

    public void StopSound(string soundToStopName){
        Sound s = Array.Find(sounds, sound => sound.soundName == soundToStopName);
        if(s == null) {
            Debug.Log("Sound:" + soundToStopName + "not found!");
            return;
        }
        s.source.Stop(); //source是一個AudioSource
    }

    //要再寫一個控制主音量的 請看notion Audio Mixer
}
