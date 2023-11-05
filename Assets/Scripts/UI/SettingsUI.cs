using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundVolume;
    [SerializeField] private string _musicKey = "musicVolume";
    [SerializeField] private string _soundKey = "soundVolume";

    void Start()
    {
        _musicVolume.value = PlayerPrefs.GetFloat(_musicKey);
        _soundVolume.value = PlayerPrefs.GetFloat(_soundKey);
    }

    public void OnChangeMusic(float volume)
    {
        PlayerPrefs.SetFloat(_musicKey, volume);
        Managers.Audio.MusicVolume = volume;
    }

    public void OnChangeSound(float volume)
    {
        PlayerPrefs.SetFloat(_soundKey, volume);
        Managers.Audio.SoundVolume = volume;
    }
}
