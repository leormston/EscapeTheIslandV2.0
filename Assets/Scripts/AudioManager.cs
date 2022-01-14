using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    public static AudioManager instance;

    public const string MUSIC_KEY = "MusicChannel";
    public const string SFX_KEY = "EffectsChannel";

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else{
            Destroy(gameObject);
        }

        LoadVolume();
    }

    void LoadVolume(){ //volume saved in VolumeSettings.cs
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0f);
        float effectsVolume = PlayerPrefs.GetFloat(SFX_KEY, 0f);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(effectsVolume) * 20);
    }
}
