using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationButton : MonoBehaviour
{
    [SerializeField] private SceneManager _sceneManager;

    void OnMouseDown()
    {
        _sceneManager.ToggleDeveloper();
    }
}
