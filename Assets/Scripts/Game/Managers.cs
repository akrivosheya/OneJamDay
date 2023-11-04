using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static AudioManager Audio { get; private set; }

    [SerializeField] private AudioManager _audio;
    [SerializeField] private string _firstScene = "MainMenu";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Audio = _audio;

        StartCoroutine(CheckReady());
    }

    private IEnumerator CheckReady()
    {
        while (!_audio.IsReady)
        {
            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(_firstScene);
    }
}
