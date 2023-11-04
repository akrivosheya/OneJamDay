using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private Text _results;
    [SerializeField] private Developer _developer;

    void Start()
    {
        _screen.SetActive(false);
    }

    public void Activate()
    {
        _screen.SetActive(true);
        _results.text = _developer.MadeGames.ToString();
    }
}
