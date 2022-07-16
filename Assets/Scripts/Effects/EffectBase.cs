using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBase : MonoBehaviour
{
    /// <summary>
    /// The values the dice can land on to activate this effect
    /// </summary>
    [SerializeField]
    private List<int> _targetValues;

    [SerializeField]
    private Dice _dice;

    protected abstract void ActivateEffect();

    private void Awake()
    {
        _dice.OnSelectedValue.AddListener(OnSelectedValue);
    }

    private void OnSelectedValue(int selectedValue)
    {
        if(_targetValues.Contains(selectedValue))
            ActivateEffect();
    }

    protected void OnValidate()
    {
        _dice = GetComponentInParent<Dice>();
    }
}
