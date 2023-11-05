using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool IsReady { get; private set; } = false;
    public bool CanChange { get => !_isChanging; }
    [SerializeField] private string _audioFolder = "Audio/";
    [SerializeField] private string _musicKey = "musicVolume";
    [SerializeField] private string _soundKey = "soundVolume";
    [SerializeField] private float _changeSpeed = 1f;
    [SerializeField] private float _defaultVolume = 0.5f;

    public float MusicVolume 
    {
        get => _mainMusicSource.volume;

        set
        {
            float volume = FixVolume(value);
            _mainMusicSource.volume = volume;
        }
    }
    public float SoundVolume 
    {
        get => _regularSoundSource.volume;

        set
        {
            float volume = FixVolume(value);
            _regularSoundSource.volume = volume;
            _oneShotSoundSource.volume = volume;
        }
    }

    [SerializeField] private AudioSource _mainMusicSource;
    [SerializeField] private AudioSource _secondMusicSource;
    [SerializeField] private AudioSource _regularSoundSource;
    [SerializeField] private AudioSource _oneShotSoundSource;
    private bool _isChanging = false;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey(_musicKey))
        {
            PlayerPrefs.SetFloat(_musicKey, _defaultVolume);
            PlayerPrefs.SetFloat(_soundKey, _defaultVolume);
        }

        MusicVolume = PlayerPrefs.GetFloat(_musicKey);
        SoundVolume = PlayerPrefs.GetFloat(_soundKey);

        _mainMusicSource.ignoreListenerPause = true;
        _secondMusicSource.ignoreListenerPause = true;
        IsReady = true;
    }

    public void PlaySound(string sound)
    {
        AudioClip clip = Resources.Load<AudioClip>(_audioFolder + sound);
        _oneShotSoundSource.PlayOneShot(clip);
    }
    
    public void PlayRegularSound(string sound)
    {
        AudioClip clip = Resources.Load<AudioClip>(_audioFolder + sound);
        _regularSoundSource.clip = clip;
        _regularSoundSource.Play();
    }
    
    public void StopRegularSound()
    {
        _regularSoundSource.Stop();
    }

    public void PlayMusic(string music)
    {
        if (_isChanging)
        {
            return;
        }

        _isChanging = true;
        AudioClip clip = Resources.Load<AudioClip>(_audioFolder + music);
        StartCoroutine(ChangeMusic(clip));
    }

    public void StopMusic()
    {
        PlayMusic(null);
    }

    private float FixVolume(float volume)
    {
        return Mathf.Clamp(volume, 0, 1);
    }

    private IEnumerator ChangeMusic(AudioClip clip)
    {
        if (clip != null)
        {
            _secondMusicSource.clip = clip;
            _secondMusicSource.Play();
        }
        _secondMusicSource.volume = 0;
        float maxVolume = _mainMusicSource.volume;
        float coeff = 0;

        while (coeff < 1)
        {
            _secondMusicSource.volume = maxVolume * coeff;
            _mainMusicSource.volume = maxVolume * (maxVolume - coeff);
            coeff += _changeSpeed * Time.unscaledDeltaTime;

            yield return null;
        }

        _secondMusicSource.volume = maxVolume;
        _mainMusicSource.volume = 0;
        _mainMusicSource.Stop();

        AudioSource tmp = _mainMusicSource;
        _mainMusicSource = _secondMusicSource;
        _secondMusicSource = tmp;
        _isChanging = false;
    }
}
