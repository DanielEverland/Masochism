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
    /// This is multiplied by the value of the dice that currently faces the camera.
    /// </summary>
    [SerializeField]
    private float _linearForce = 100.0f;

    /// <summary>
    /// The amount of torque added when jumping
    /// </summary>
    [SerializeField]
    private float _torqueMagnitude = 10.0f;

    public void Jump(Vector2 mouseDirection)
    {
        // We don't care about the magnitude
        mouseDirection.Normalize();
        
        AddLinearInput(mouseDirection);
        AddAngularImpulse();
    }

    private void AddLinearInput(Vector2 jumpDirection)
    {
        _rigidBody.AddForce(jumpDirection * _linearForce * _dice.BestSide.Number);
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

        _rigidBody.AddTorque(torque * _torqueMagnitude);
    }

    private void OnValidate()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _dice = GetComponent<Dice>();
    }
}
