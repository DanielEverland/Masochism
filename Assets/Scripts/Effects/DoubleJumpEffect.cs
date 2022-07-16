using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpEffect : EffectBase
{
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private int _timesCanJumpBeforeStoppingAmount = 2;
    
    private bool _isEffectActive;
    private bool _isArmed;

    protected override void ActivateEffect()
    {
        _isEffectActive = true;
        _isArmed = false;
        _playerController.TimesCanJumpBetweenStops = _timesCanJumpBeforeStoppingAmount;
    }

    private void Start()
    {
        DiceComponent.OnSelectedValue.AddListener(OnSelectedNewValue);
    }

    private void OnSelectedNewValue(int newValue)
    {
        if (!_isEffectActive)
            return;

        if (!_isArmed)
        {
            _isArmed = true;
        }
        else
        {
            _playerController.TimesCanJumpBetweenStops = 1;
            _isEffectActive = false;
            _isArmed = false;
        }
    }

    private new void OnValidate()
    {
        base.OnValidate();

        _playerController = GetComponentInParent<PlayerController>();
    }
}
