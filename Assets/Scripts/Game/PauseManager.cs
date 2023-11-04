using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseWindow;

    void Start()
    {
        UnPause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _pauseWindow.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        _pauseWindow.SetActive(false);
    }
}
