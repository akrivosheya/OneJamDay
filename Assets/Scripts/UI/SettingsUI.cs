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

    [SerializeField] private float _defaultVolume = 0.5f;

    void Start()
    {
        if (!PlayerPrefs.HasKey(_musicKey))
        {
            PlayerPrefs.SetFloat(_musicKey, _defaultVolume);
            PlayerPrefs.SetFloat(_soundKey, _defaultVolume);
        }

        _musicVolume.value = PlayerPrefs.GetFloat(_musicKey);
        _soundVolume.value = PlayerPrefs.GetFloat(_soundKey);
        Managers.Audio.MusicVolume = PlayerPrefs.GetFloat(_musicKey);
        Managers.Audio.SoundVolume = PlayerPrefs.GetFloat(_soundKey);
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
