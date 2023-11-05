using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hours : MonoBehaviour
{
    public float Seconds { get; private set; } = 0;
    public float MaxSeconds { get => _maxSeconds; }
    
    [SerializeField] private SceneManager _sceneManager;
    [SerializeField] private Developer _developer;
    [SerializeField] private float _maxSeconds = 20f;

    private UnityEvent _onChange = new UnityEvent();

    public void AddListener(UnityAction action)
    {
        _onChange.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        _onChange.RemoveListener(action);
    }

    void Update()
    {
        if (_developer.IsActivated)
        {
            Seconds += Time.deltaTime;
            _onChange.Invoke();

            if (_developer.GetStat(Developer.Stats.Progress) >= _developer.MaxStat)
            {
                _sceneManager.CompleteGame();
                Seconds = 0;
            }
            else if (_maxSeconds <= Seconds)
            {
                _sceneManager.StopDeveloping(SceneManager.GameEnds.TimeOut);
            }
        }
    }
}
