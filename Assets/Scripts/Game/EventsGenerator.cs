using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsGenerator : MonoBehaviour
{
    [SerializeField] private Developer _developer;
    [SerializeField] private NotificationUI _notification;
    [SerializeField] private Dictionary<Developer.Stats, string> _events = new Dictionary<Developer.Stats, string>();

    [SerializeField] private string _energyEvent = "Время прокрастинации";
    [SerializeField] private string _hungerEvent = "Внезапный жор";
    [SerializeField] private string _toiletEvent = "Слабый пузырь";

    private List<Developer.Stats> _stats = new List<Developer.Stats>();
    private int _madeGames = 0;


    void Awake()
    {
        _developer.AddListener(OnDeveloperChange);
    }

    void OnDestroy()
    {
        _developer.RemoveListener(OnDeveloperChange);
    }

    void Start()
    {
        _stats.Add(Developer.Stats.Energy);
        _stats.Add(Developer.Stats.Hunger);
        _stats.Add(Developer.Stats.Wastes);

        _events.Add(Developer.Stats.Energy, _energyEvent);
        _events.Add(Developer.Stats.Hunger, _hungerEvent);
        _events.Add(Developer.Stats.Wastes, _toiletEvent);
    }

    private void OnDeveloperChange()
    {
        if (_developer.MadeGames > _madeGames && _developer.MadeGames % 3 == 0)
        {
            _madeGames = _developer.MadeGames;
            int statIndex = Random.Range(0, _stats.Count);
            Developer.Stats stat = _stats[statIndex];
            float currentStatFloat = _developer.GetStat(stat);

            switch (stat)
            {
                case Developer.Stats.Energy:
                    _developer.ChangeStat(stat, -(currentStatFloat / 2));
                    break;
                case Developer.Stats.Hunger:
                case Developer.Stats.Wastes:
                    _developer.ChangeStat(stat, (_developer.MaxStat - currentStatFloat) / 2);
                    break;
            }

            _notification.SetText(_events[stat]);
            _notification.Notify();
        }
    }
}
