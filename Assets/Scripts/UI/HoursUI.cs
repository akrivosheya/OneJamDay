using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoursUI : MonoBehaviour
{
    [SerializeField] private RectTransform _anchor;
    [SerializeField] private RectTransform _hoursUI;
    [SerializeField] private Hours _hours;
    [SerializeField] private float _minScale = 1;
    [SerializeField] private float _maxScale = 2;


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
        _anchor.localEulerAngles = new Vector3(0, 0, -360 * _hours.Seconds / _hours.MaxSeconds);
        float currentScale = (_maxScale - _minScale) * _hours.Seconds / _hours.MaxSeconds + _minScale;
        _hoursUI.localScale = new Vector3(currentScale, currentScale, 0);
    }
}
