using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _rulesWindow;
    [SerializeField] private GameObject _authorsWindow;

    [SerializeField] private string _gameScene;
    [SerializeField] private string _menuMusic = "MainMenuMusic";

    private GameObject _currentWindow;

    void Start()
    {
        Managers.Audio.PlayMusic(_menuMusic);
        _mainWindow.SetActive(true);
        _settingsWindow.SetActive(false);
        _rulesWindow.SetActive(false);
        _authorsWindow.SetActive(false);
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
        _currentWindow = _settingsWindow;
    }
    
    public void OnRules()
    {
        _mainWindow.SetActive(false);
        _rulesWindow.SetActive(true);
        _currentWindow = _rulesWindow;
    }
    
    public void OnAuthors()
    {
        _mainWindow.SetActive(false);
        _authorsWindow.SetActive(true);
        _currentWindow = _authorsWindow;
    }
    
    public void OnBack()
    {
        _mainWindow.SetActive(true);
        _currentWindow.SetActive(false);
    }
    
    public void OnExit()
    {
        Application.Quit();
    }
}
