using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _block;

    [SerializeField] private string _gameScene;
    [SerializeField] private string _menuMusic = "MainMenuMusic";

    void Start()
    {
        Managers.Audio.PlayMusic(_menuMusic);
        _mainWindow.SetActive(true);
        _settingsWindow.SetActive(false);
        _block.SetActive(false);
    }

    public void OnStartGame()
    {
        if (!Managers.Audio.CanChange)
        {
            return;
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(_gameScene);
    }
    
    public void OnSettings()
    {
        _mainWindow.SetActive(false);
        _settingsWindow.SetActive(true);
    }
    
    public void OnBack()
    {
        _mainWindow.SetActive(true);
        _settingsWindow.SetActive(false);
    }
    
    public void OnExit()
    {
        Application.Quit();
    }
}
