using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EffectBase : MonoBehaviour
{
    /// <summary>
    /// The values the dice can land on to activate this effect
    /// </summary>
    [SerializeField]
    private List<int> _targetValues;

    [SerializeField]
    private Dice _dice;

    [SerializeField]
    private UnityEvent OnActivated;

    protected Dice DiceComponent => _dice;

    protected abstract void ActivateEffect();

    protected virtual void Awake()
    {
        _dice.OnSelectedValue.AddListener(OnSelectedValue);
    }

    private void OnSelectedValue(int selectedValue)
    {
        if (_targetValues.Contains(selectedValue))
        {
            ActivateEffect();
            OnActivated.Invoke();
        }
    }

    protected void OnValidate()
    {
        _dice = GetComponentInParent<Dice>();
    }
}
