using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private List<GameObject> _buttons;
    [SerializeField] private Developer _developer;

    [SerializeField] private NotificationUI _finishedGameNotification;
    [SerializeField] private FinishScreenUI _finishScreen;

    [SerializeField] private Vector3 _kitchenPosition;
    [SerializeField] private Vector3 _showerPosition;
    [SerializeField] private Vector3 _workPosition;
    [SerializeField] private Vector3 _bedroomPosition;

    [SerializeField] private float _moveSpeed = 0.1f;
    [SerializeField] private string _gameMusic = "GameMusic";
    private Dictionary<Positions, Vector3> _positions = new Dictionary<Positions, Vector3>();

    public enum Positions
    {
        Kitchen,
        Shower,
        Work,
        Bedroom,
    }

    public enum GameEnds
    {
        TimeOut,
        EnergyOut,
        HungerOverflow,
        WastesOverflow,
    }

    void Start() {
        Managers.Audio.PlayMusic(_gameMusic);

        _positions.Add(Positions.Kitchen, _kitchenPosition);
        _positions.Add(Positions.Shower, _showerPosition);
        _positions.Add(Positions.Work, _workPosition);
        _positions.Add(Positions.Bedroom, _bedroomPosition);
    }

    public void MoveTo(Positions position)
    {
        SetButtonsActive(false);
        _developer.IsActivated = false;
        _developer.MoveToPosition(position);
        StartCoroutine(MoveSceneTo(_positions[position]));
    }

    public void ToggleDeveloper()
    {
        _developer.IsActivated = !_developer.IsActivated;
    }

    public void StopDeveloping(GameEnds end)
    {
        SetButtonsActive(false);
        _developer.IsActivated = false;
        _finishScreen.Activate(end);
    }

    public void CompleteGame()
    {
        _finishedGameNotification.Notify();
        _developer.ResetStat(Developer.Stats.Progress);
        _developer.FinishGame();
    }

    public void LoadScene(string scene)
    {
        //возможно нужно вернуть timeScale
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    private void SetButtonsActive(bool active)
    {
        foreach (GameObject button in _buttons)
        {
            button.SetActive(active);
        }
    }

    private IEnumerator MoveSceneTo(Vector3 position)
    {
        Vector3 firstPosition = _camera.transform.position;
        Vector3 offset = position - firstPosition;
        float positionMul = 0;

        while (positionMul < 1)
        {
            _camera.transform.position = firstPosition + offset * positionMul;
            positionMul += _moveSpeed * Time.deltaTime;
            yield return null;
        }

        _camera.transform.position = firstPosition + offset;
        SetButtonsActive(true);
    }
}
