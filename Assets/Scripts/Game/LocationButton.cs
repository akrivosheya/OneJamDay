using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationButton : MonoBehaviour
{
    [SerializeField] private SceneManager _sceneManager; 
    [SerializeField] private SceneManager.Positions _position;
    [SerializeField] private string _movingSound = "Whoosh";

    void OnMouseDown()
    {
        Managers.Audio.PlaySound(_movingSound);
        _sceneManager.MoveTo(_position);
    }
}
