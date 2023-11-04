using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] private GameObject _textContainer;
    [SerializeField] private Text _text;
    [SerializeField] private float _waitTime = 5f;

    private bool _isNotifying = false;

    void Start()
    {
        _textContainer.gameObject.SetActive(false);
    }

    public void Notify()
    {
        if (_isNotifying)
        {
            return;
        }

        _isNotifying = true;
        _textContainer.SetActive(true);

        StartCoroutine(HideText());
    }

    public void SetText(string text)
    {
        if (_isNotifying)
        {
            return;
        }

        _text.text = text;
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(_waitTime);

        _textContainer.SetActive(false);
        _isNotifying = false;
    }
}
