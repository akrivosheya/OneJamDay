using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="DeveloperStateSO")]
public class DeveloperState : ScriptableObject
{
    [SerializeField] private float _energyCoeff;
    [SerializeField] private float _hungerCoeff;
    [SerializeField] private float _wastesCoeff;
    [SerializeField] private float _progressCoeff;
    [SerializeField] private float _commonMul = 0.1f;

    public void UpdateState(Developer developer)
    {
        developer.ChangeStat(Developer.Stats.Energy, _energyCoeff * _commonMul * Time.deltaTime);
        developer.ChangeStat(Developer.Stats.Hunger, _hungerCoeff * _commonMul * Time.deltaTime);
        developer.ChangeStat(Developer.Stats.Wastes, _wastesCoeff * _commonMul * Time.deltaTime);
        developer.ChangeStat(Developer.Stats.Progress, _progressCoeff * _commonMul * Time.deltaTime);
    }
}
