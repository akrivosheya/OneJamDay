using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Developer : MonoBehaviour
{
    public SceneManager.Positions CurrentPosition { private get; set; }
    public bool IsActivated { get; set; } = false;
    public int MadeGames { get; private set; } = 0;

    public readonly float MaxStat = 100;

    [SerializeField] private NotificationUI _developersMind;

    [SerializeField] private float _initEnergy;
    [SerializeField] private float _initHunger;
    [SerializeField] private float _initWastes;
    [SerializeField] private float _initProgress = 0f;

    [SerializeField] private DeveloperState _workState;
    [SerializeField] private DeveloperState _kitchenState;
    [SerializeField] private DeveloperState _showerState;
    [SerializeField] private DeveloperState _bedroomState;

    [SerializeField] private List<string> _madeGameMinds;
    [SerializeField] private List<string> _hungryMinds;
    [SerializeField] private List<string> _toiletMinds;
    [SerializeField] private List<string> _lowEnergyMinds;

    private DeveloperState _currentState;
    private Dictionary<Stats, float> _stats = new Dictionary<Stats, float>();
    private Dictionary<SceneManager.Positions, DeveloperState> _states = new Dictionary<SceneManager.Positions, DeveloperState>();
    private Dictionary<Stats, List<string>> _allDevelopersMinds = new Dictionary<Stats, List<string>>();
    private UnityEvent _onChange = new UnityEvent();

    public enum Stats
    {
        Energy,
        Hunger,
        Wastes,
        Progress,
    }


    void Start()
    {
        _stats.Add(Stats.Energy, _initEnergy);
        _stats.Add(Stats.Hunger, _initHunger);
        _stats.Add(Stats.Wastes, _initWastes);
        _stats.Add(Stats.Progress, _initProgress);

        _allDevelopersMinds.Add(Stats.Energy, _lowEnergyMinds);
        _allDevelopersMinds.Add(Stats.Hunger, _hungryMinds);
        _allDevelopersMinds.Add(Stats.Wastes, _toiletMinds);
        _allDevelopersMinds.Add(Stats.Progress, _madeGameMinds);

        _states.Add(SceneManager.Positions.Kitchen, _kitchenState);
        _states.Add(SceneManager.Positions.Shower, _showerState);
        _states.Add(SceneManager.Positions.Work, _workState);
        _states.Add(SceneManager.Positions.Bedroom, _bedroomState);

        _currentState = _workState;
        _onChange.Invoke();
    }

    void Update()
    {
        if (IsActivated)
        {
            _currentState.UpdateState(this);
        }
    }

    public void ChangeStat(Stats stat, float change)
    {
        float previousStat = _stats[stat];
        _stats[stat] += change;

        if (_stats[stat] < 0)
        {
            _stats[stat] = 0;
        }
        else if (_stats[stat] > MaxStat)
        {
            _stats[stat] = MaxStat;
        }

        _onChange.Invoke();

        switch (stat) 
        {
            case Stats.Energy:
                if (_stats[stat] <= MaxStat / 2 && previousStat >= MaxStat / 2)
                {
                    ShowMind(stat);
                }
                break;
            case Stats.Hunger:
            case Stats.Wastes:
                if (_stats[stat] >= MaxStat / 2 && previousStat <= MaxStat / 2)
                {
                    ShowMind(stat);
                }
                break;
        }
    }

    public void ResetStat(Stats stat)
    {
        _stats[stat] = 0;

        if (stat == Stats.Progress)
        {
            ShowMind(stat);
        }

        _onChange.Invoke();
    }

    public float GetStat(Stats stat)
    {
        return _stats[stat];
    }

    public void MoveToPosition(SceneManager.Positions position)
    {
        _currentState = _states[position];
    }

    public void FinishGame()
    {
        MadeGames++;
    }
    
    public void AddListener(UnityAction action)
    {
        _onChange.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        _onChange.RemoveListener(action);
    }

    private void ShowMind(Stats stat)
    {
        List<string> currentMinds = _allDevelopersMinds[stat];
        string mind = currentMinds[Random.Range(0, currentMinds.Count - 1)];
        _developersMind.SetText(mind);
        _developersMind.Notify();
    }
}
