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

    [SerializeField] private string _hungrySound;
    [SerializeField] private string _toiletSound;
    [SerializeField] private string _lowEnergySound;
    
    private Dictionary<Developer.Stats, string> _allSounds = new Dictionary<Developer.Stats, string>();

    private Dictionary<Developer.Stats, Sprite> _emotions = new Dictionary<Developer.Stats, Sprite>();

    void Awake()
    {
        _developer.AddListener(OnDeveloperChange);

        _emotions.Add(Developer.Stats.Energy, _sleepySprite);
        _emotions.Add(Developer.Stats.Hunger, _hungrySprite);
        _emotions.Add(Developer.Stats.Wastes, _toiletSprite);

        _allSounds.Add(Developer.Stats.Energy, _lowEnergySound);
        _allSounds.Add(Developer.Stats.Hunger, _hungrySound);
        _allSounds.Add(Developer.Stats.Wastes, _toiletSound);
    }

    void OnDestroy()
    {
        _developer.RemoveListener(OnDeveloperChange);
    }

    private void OnDeveloperChange()
    {
        Sprite previousSprite = _image.sprite;

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

        if (previousSprite != _image.sprite)
        {
            if (_image.sprite == _normalSprite)
            {
                return;
            }

            Managers.Audio.PlaySound(_allSounds[minStat]);
        }
    }
}
