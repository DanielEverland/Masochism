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
    private Jumper _jumper;

    protected override void ActivateEffect()
    {
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
