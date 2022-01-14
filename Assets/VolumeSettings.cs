using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;

    public const string MIXER_MUSIC = "MusicChannel";
    public const string MIXER_SFX = "EffectsChannel";

    void Awake(){
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetFXVolume);
    }

    void Start(){
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 0f);
        effectsSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 0f);
    }

    void SetMusicVolume(float value){
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetFXVolume(float value){
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    void OnDisable(){
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, effectsSlider.value);
    }
}
