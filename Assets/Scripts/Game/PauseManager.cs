using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseWindow;
    private bool _paused = false;

    void Start()
    {
        UnPause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_paused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        _paused = true;
        Time.timeScale = 0;
        _pauseWindow.SetActive(true);
    }

    public void UnPause()
    {
        _paused = false;
        Time.timeScale = 1;
        _pauseWindow.SetActive(false);
    }
}
