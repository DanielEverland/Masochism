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
        // We don't care about the magnitude
        mouseDirection.Normalize();
        EnablePhysics();
        AddLinearInput(mouseDirection);
        AddAngularImpulse();
    }

    private void AddLinearInput(Vector2 jumpDirection)
    {
        _rigidBody.AddForce(jumpDirection * _linearForce.Evaluate(_dice.BestSide.Number));
    }

    private void AddAngularImpulse()
    {
        // Randomly choose a "direction" to rotate cube
        Vector3 torque = new Vector3
        {
            x = Random.Range(0.0f, 1.0f),
            y = Random.Range(0.0f, 1.0f),
            z = Random.Range(0.0f, 1.0f),
        };

        _rigidBody.AddTorque(torque * _torqueMagnitude.Evaluate(_dice.BestSide.Number));
    }

    private void OnValidate()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _dice = GetComponent<Dice>();
    }

    private void EnablePhysics()
    {
        _rigidBody.isKinematic = false;
    }
}
