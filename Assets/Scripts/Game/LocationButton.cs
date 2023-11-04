using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationButton : MonoBehaviour
{
    [SerializeField] private SceneManager _sceneManager; 
    [SerializeField] private SceneManager.Positions _position;

    void OnMouseDown()
    {
        _sceneManager.MoveTo(_position);
    }
}
