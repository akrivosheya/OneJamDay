using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class DeveloperInRoom : MonoBehaviour
{
    [SerializeField] private Developer _developer;
    [SerializeField] private Animator _animator;
    [SerializeField] private SceneManager.Positions _stateForActivation;
    [SerializeField] private string _activationKey = "Activated";


    void Awake()
    {
        _developer.AddListener(OnDeveloperChange);
    }

    void OnDestroy()
    {
        _developer.RemoveListener(OnDeveloperChange);
    }

    public void OnDeveloperChange()
    {
        if (_developer.State == _stateForActivation)
        {
            if (_developer.IsActivated)
            {
                _animator.SetBool(_activationKey, true);
            }
            else
            {
                _animator.SetBool(_activationKey, false);
            }
        }
    }
}
