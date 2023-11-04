using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hours : MonoBehaviour
{
    public readonly float MAX_SECONDS = 20f;
    public float Seconds { get; private set; } = 0;
    [SerializeField] private SceneManager _sceneManager;
    [SerializeField] private Developer _developer;

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
            else if (MAX_SECONDS <= Seconds)
            {
                _sceneManager.StopDeveloping();
            }
        }
    }
}
