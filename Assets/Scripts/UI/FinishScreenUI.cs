using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private Image _endSprite;
    [SerializeField] private Text _results;
    [SerializeField] private Developer _developer;

    [SerializeField] private Sprite _timeOutSprite;
    [SerializeField] private Sprite _hungrySprite;
    [SerializeField] private Sprite _energySprite;
    [SerializeField] private Sprite _toiletSprite;
    
    [SerializeField] private List<string> _timeOutString;
    [SerializeField] private List<string> _hungryString;
    [SerializeField] private List<string> _energyString;
    [SerializeField] private List<string> _toiletString;

    private Dictionary<SceneManager.GameEnds, Sprite> _endSprites = new Dictionary<SceneManager.GameEnds, Sprite>();
    private Dictionary<SceneManager.GameEnds, List<string>> _endStrings = new Dictionary<SceneManager.GameEnds, List<string>>();

    void Start()
    {
        _endSprites.Add(SceneManager.GameEnds.TimeOut, _timeOutSprite);
        _endSprites.Add(SceneManager.GameEnds.HungerOverflow, _hungrySprite);
        _endSprites.Add(SceneManager.GameEnds.EnergyOut, _energySprite);
        _endSprites.Add(SceneManager.GameEnds.WastesOverflow, _toiletSprite);
        
        _endStrings.Add(SceneManager.GameEnds.TimeOut, _timeOutString);
        _endStrings.Add(SceneManager.GameEnds.HungerOverflow, _hungryString);
        _endStrings.Add(SceneManager.GameEnds.EnergyOut, _energyString);
        _endStrings.Add(SceneManager.GameEnds.WastesOverflow, _toiletString);

        _screen.SetActive(false);
    }

    public void Activate(SceneManager.GameEnds end)
    {
        _screen.SetActive(true);
        List<string> currentTexts = _endStrings[end];
        _results.text = currentTexts[Random.Range(0, currentTexts.Count - 1)];
        _results.text = $"{currentTexts[Random.Range(0, currentTexts.Count - 1)]}\nСделанные игры: {_developer.MadeGames}";
        _endSprite.sprite = _endSprites[end];
    }
}
