using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : EffectBase
{
    [SerializeField]
    private float MinimumYComponent = 0.5f;

    [SerializeField]
    private float MaximumComponent = 2.0f;

    [SerializeField]
    private float ForceMagnitude = 1000.0f;

    [SerializeField]
    private float ExplosionDelay = 1.0f;

    [SerializeField]
    private Jumper _jumper;

    [SerializeField]
    private Shaker _shaker;

    private float _explodeTime;
    private bool _isArmed;
    
    protected override void ActivateEffect()
    {
        DiceComponent.Block();

        _isArmed = true;
        _explodeTime = Time.time + ExplosionDelay;
        _shaker.Toggle(true);
    }

    private void Update()
    {
        if (!_isArmed)
            return;

        if(Time.time > _explodeTime)
            Explode();
    }

    private void Explode()
    {
        _isArmed = false;

        _shaker.Toggle(false);
        DiceComponent.Unblock();

        float xComponent = Random.Range(0.0f, 1.0f) > 0.5f ? 1.0f : -1.0f;
        float yComponent = Random.Range(MinimumYComponent, MaximumComponent);

        Vector2 forceDirection = new Vector2(xComponent, yComponent);
        forceDirection *= ForceMagnitude;
        
        _jumper.Impulse(forceDirection);
    }

    private new void OnValidate()
    {
        base.OnValidate();

        _jumper = GetComponentInParent<Jumper>();
    }
}
