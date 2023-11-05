using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoursUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Hours _hours;


    void Awake()
    {
        _hours.AddListener(OnHoursChange);
    }

    void OnDestroy()
    {
        _hours.RemoveListener(OnHoursChange);
    }

    void OnHoursChange()
    {
        _slider.value = _hours.Seconds / _hours.MaxSeconds;
    }
}
