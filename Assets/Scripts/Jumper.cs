using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Dice))]
public class Jumper : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody;

    [SerializeField]
    private Dice _dice;

    /// <summary>
    /// Magnitude applied linearly when jumping.
    /// The selected value is the value of the dice.
    /// </summary>
    [SerializeField]
    private AnimationCurve _linearForce;

    /// <summary>
    /// The amount of torque added when jumping
    /// The selected value is the value of the dice.
    /// </summary>
    [SerializeField]
    private AnimationCurve _torqueMagnitude;

    public void Jump(Vector2 mouseDirection)
    {
        if (_dice.State == DiceState.Blocked)
            return;

        // We don't care about the magnitude
        mouseDirection.Normalize();
        
        AddLinearInput(mouseDirection * _linearForce.Evaluate(_dice.BestSide.Number));
        AddAngularImpulse(_torqueMagnitude.Evaluate(_dice.BestSide.Number));
    }

    public void Impulse(Vector2 impulse)
    {
        AddLinearInput(impulse);
        AddAngularImpulse(impulse.magnitude);
    }

    private void AddLinearInput(Vector2 linearImpulse)
    {
        _rigidBody.AddForce(linearImpulse);
    }

    private void AddAngularImpulse(float magnitude)
    {
        // Randomly choose a "direction" to rotate cube
        Vector3 torque = new Vector3
        {
            x = Random.Range(0.0f, 1.0f),
            y = Random.Range(0.0f, 1.0f),
            z = Random.Range(0.0f, 1.0f),
        };

        _rigidBody.AddTorque(torque * magnitude);
    }

    private void OnValidate()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _dice = GetComponent<Dice>();
    }
}
