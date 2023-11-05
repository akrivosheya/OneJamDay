using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionsUI : MonoBehaviour
{
    [SerializeField] private Developer _developer;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _hungrySprite;
    [SerializeField] private Sprite _toiletSprite;
    [SerializeField] private Sprite _sleepySprite;

    private Dictionary<Developer.Stats, Sprite> _emotions = new Dictionary<Developer.Stats, Sprite>();

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
        _emotions.Add(Developer.Stats.Energy, _sleepySprite);
        _emotions.Add(Developer.Stats.Hunger, _hungrySprite);
        _emotions.Add(Developer.Stats.Wastes, _toiletSprite);
    }

    private void OnDeveloperChange()
    {
        float energy = _developer.GetStat(Developer.Stats.Energy);
        float hunger = _developer.GetStat(Developer.Stats.Hunger);
        float wastes = _developer.GetStat(Developer.Stats.Wastes);
        float minStatFloat = energy;
        Developer.Stats minStat = Developer.Stats.Energy;

        if (_developer.MaxStat - hunger < minStatFloat) {
            minStatFloat = _developer.MaxStat - hunger;
            minStat = Developer.Stats.Hunger;
        }
        if (_developer.MaxStat - wastes < minStatFloat) {
            minStatFloat = _developer.MaxStat - wastes;
            minStat = Developer.Stats.Wastes;
        }

        if (minStatFloat < _developer.MaxStat / 2)
        {
            _image.sprite = _emotions[minStat];
        }
        else
        {
            _image.sprite = _normalSprite;
        }
    }
}
