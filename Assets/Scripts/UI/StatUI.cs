using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] private Developer _developer;
    [SerializeField] private Developer.Stats _stat;
    [SerializeField] private Slider _slider;

    void Awake()
    {
        _developer.AddListener(OnDeveloperChange);
    }

    void OnDestroy()
    {
        _developer.RemoveListener(OnDeveloperChange);
    }

    private void OnDeveloperChange()
    {
        _slider.value = _developer.GetStat(_stat) / _developer.MaxStat;
    }
}
